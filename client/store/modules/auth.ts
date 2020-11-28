import { Mutation, Action, VuexModule, getModule, Module } from 'vuex-module-decorators'
import { store } from '~/store'
import AuthTokenModel from '~/api/models/AuthTokenModel'
import { $api } from '~/globals/api'

@Module({
  dynamic: true,
  store,
  name: 'auth',
  stateFactory: true,
  namespaced: true
})
class AuthModule extends VuexModule {
  // state
  _token?: AuthTokenModel

  // getters
  public get token() {
    return this._token
  }

  public get authenticated() {
    console.log(this._token)
    return Boolean(this._token)
  }

  // mutations
  @Mutation
  private setToken(token: AuthTokenModel) {
    this._token = token
  }

  // actions
  @Action
  public async getToken(username: string, password: string) {
    const response = await $api.auth.getToken(username, password)
    if (response.successful) {
      this.context.commit('setToken', response.data!)
    }
  }
}

export default getModule(AuthModule)
