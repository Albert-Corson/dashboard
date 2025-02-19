import { NuxtAxiosInstance } from '@nuxtjs/axios'
import WidgetModel from './models/WidgetModel'
import ResponseModel from './models/ResponseModel'

export interface IWidgetRepository {

  /**
   * List all widgets available
   * 
   * To list widgets of a specific service, provide the serviceId parameter
   *
   * @param serviceId id of service to list widgets of
   */
  listWidgets(serviceId?: number): Promise<ResponseModel<Array<WidgetModel>>>

  /**
   * List all widgets the currently logged in user is registered to
   * 
   * @param serviceId id of service to list registered widgets of
   */
  listRegisteredWidgets(serviceId?: number): Promise<ResponseModel<Array<WidgetModel>>>
  
  /**
   * Get a widget by id
   * 
   * @param widgetId widget id
   * @param params optional query parameters
   */
  fetchWidgetData(widgetId: number, params?: object): Promise<ResponseModel<WidgetModel>>

  /**
   * Register the user to a widget
   * 
   * @param widgetId widget id
   */
  registerWidget(widgetId: number): Promise<ResponseModel>

  /**
   * Unregister the user from a widget
   * 
   * @param widgetId widget id
   */
  unregisterWidget(widgetId: number): Promise<ResponseModel>
}

const makeWidgetRepository = ($axios: NuxtAxiosInstance): IWidgetRepository => ({

  listWidgets(serviceId?: number): Promise<ResponseModel<Array<WidgetModel>>> {
    if (serviceId) {
      return $axios.$get(`/widgets?serviceId=${serviceId}`)
    } else {
      return $axios.$get(`/widgets`)
    }
  },

  listRegisteredWidgets(serviceId?: number): Promise<ResponseModel<Array<WidgetModel>>>
  {
    if (serviceId) {
      return $axios.$get(`/widgets/me?serviceId=${serviceId}`)
    } else {
      return $axios.$get(`/widgets/me`)
    }
  },

  fetchWidgetData(widgetId: number, params?: object): Promise<ResponseModel<WidgetModel>> {
    let url = `/widgets/${widgetId}`
    let qparams = ''

    if (params) {
      for (let key of Object.keys(params)) {
        qparams += qparams === '' ? '?' : '&'
        // @ts-ignore
        qparams += `${key}=${params[key]}`
      }
    }
    url += qparams
    return $axios.$get(url)
  },

  registerWidget(widgetId: number): Promise<ResponseModel> {
    return $axios.$post(`/widgets/${widgetId}`)
  },

  unregisterWidget(widgetId: number): Promise<ResponseModel> {
    return $axios.$delete(`/widgets/${widgetId}`)
  }

})

export default makeWidgetRepository
