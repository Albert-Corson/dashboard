using System;
using Dashboard.API.Exceptions.Http;
using Dashboard.API.Models;
using Dashboard.API.Services.Services;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Enums;
using Microsoft.AspNetCore.Http;

namespace Dashboard.API.Services.Widgets.Imgur
{
    public class ImgurGallerySearchWidgetService : IWidgetService
    {
        public ImgurGallerySearchWidgetService(ImgurServiceService imgur)
        {
            Imgur = imgur;
        }

        private ImgurServiceService Imgur { get; }

        public string Name { get; } = "Imgur gallery search";

        public void CallWidgetApi(HttpContext context, WidgetCallParameters widgetCallParams, ref WidgetCallResponseModel response)
        {
            if (Imgur.Client == null)
                throw new InternalServerErrorHttpException();
            var galleryEndpoint = new GalleryEndpoint(Imgur.Client);

            var sortStr = widgetCallParams.Strings["sort"];
            if (!Enum.TryParse<GallerySortOrder>(sortStr, true, out var sort))
                throw new BadRequestHttpException($"Query parameter `sort` has an invalid value `{sortStr}`. Expected time|viral|top");

            var task = galleryEndpoint.SearchGalleryAsync(widgetCallParams.Strings["query"], sort);
            task.Wait();

            if (!task.IsCompletedSuccessfully)
                throw new InternalServerErrorHttpException("Couldn't not reach Imgur's API");

            response.Items = ImgurServiceService.WidgetResponseItemsFromGallery(task.Result);
        }
    }
}
