<template>
  <div class="widget-list">
    <mosaic-list>
      <widget-list-item v-for="widget in widgets" :key="widget.id"
               :id="widget.id"
               :name="widget.name"
               :service="widget.service"
               v-on:select="select"
               />
    </mosaic-list>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import MosaicList from '~/components/MosaicList.vue'
import WidgetListItem from '~/components/widgets/WidgetListItem.vue'
import WidgetModel from '~/api/models/WidgetModel'

@Component({
  name: 'WidgetList',
  components: {
    MosaicList,
    WidgetListItem
  }
})
export default class WidgetList extends Vue {
  // props
  @Prop({ required: true }) readonly widgets!: Array<WidgetModel>

  // methods
  public select(id: number) {
    this.$emit('select', id)
  }
}

</script>
