<template>
  <clickable><hoverable>
    <div class="widget-list-item" @click="select">
      <icon :src="icon" width="70" height="70"/>
      <div class="widget-name">{{ name }}</div>
    </div>
  </hoverable></clickable>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import Icon from '~/components/Icon.vue'
import Clickable from '~/components/Clickable.vue'
import Hoverable from '~/components/Hoverable.vue'
import ServiceModel from '~/api/models/ServiceModel'

@Component({
  name: 'WidgetListItem',
  components: {
    Icon,
    Clickable,
    Hoverable
  }
})
export default class WidgetListItem extends Vue {
  // props
  @Prop({ required: true }) readonly id!: number
  @Prop({ required: true }) readonly name!: string
  @Prop({ required: true }) readonly service!: ServiceModel

  // computed
  get icon() {
    return '/svg/widget.svg'
  }

  // methods
  public select() {
    this.$emit('select', this.id)
  }
}
</script>

<style lang="scss" scoped>
.widget-list-item {
  position: relative;
  display: inline-block;
  padding: .5em 1em;
  text-align: center;

  a {
    color: inherit;
    text-decoration: none;
  }

  .widget-name {
    text-align: center;
  }
}
</style>
