using System.Linq;
using CatApiWrapper;
using CatApiWrapper.RequestBuilders;
using Dashboard.API.Exceptions.Http;
using Dashboard.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Dashboard.API.Services.Widgets.CatApi
{
    public class CatApiRandomImagesWidgetService : IWidgetService
    {
        private readonly string _apiKey;

        public CatApiRandomImagesWidgetService(IConfiguration configuration)
        {
            var catApiConf = configuration.GetSection("WidgetApiKeys").GetSection("CatApi");
            _apiKey = catApiConf["ApiKey"];
            Client = new CatClient();
        }

        private CatClient Client { get; }

        public string Name { get; } = "Random cat images";

        public void CallWidgetApi(HttpContext context, WidgetCallParameters widgetCallParams, ref WidgetCallResponseModel response)
        {
            try {
                var request = new GetRequestBuilder()
                    .WithApiKey(_apiKey)
                    .WithResultsPerPage(50);

                var images = Client.GetImages(request);

                if (images == null)
                    throw new InternalServerErrorHttpException("Could not reach The Cat Api");

                response.Items = images.Select(image => new WidgetCallResponseItemModel {
                    Image = image.Url,
                    Link = image.SourceUrl
                });
            } catch {
                throw new InternalServerErrorHttpException();
            }
        }
    }
}
