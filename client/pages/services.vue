<template>
  <div class="services page">
    <service-list :services="services"/>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { ServiceStore } from '~/store'
import ServiceList from '~/components/services/ServiceList.vue'
import ExtendedServiceModel from '~/components/services/ExtendedServiceModel'
import ServiceModel from '~/api/models/ServiceModel'

@Component({
  components: {
    ServiceList
  }
})
export default class ServicesPage extends Vue {
  // methods
  public reload() {
    ServiceStore.fetchServices()
    ServiceStore.fetchRegisteredServices()
  }

  // computed
  public get services(): Array<ExtendedServiceModel> {
    const allServices = ServiceStore.services
    const registeredServices = ServiceStore.registeredServices

    return allServices.map((service) => {
      let extended : ExtendedServiceModel = service
      extended.empty = false
      extended.registered = registeredServices.some((knownService) => {
        return knownService.id === service.id
      })
      return extended
    })
  }

  // hooks
  mounted() {
    this.reload()
  }
}
</script>
