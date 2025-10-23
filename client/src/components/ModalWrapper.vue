<script setup>
defineProps({
  modalId:{type: String, default: "modal-wrapper"},
  showClose: {type: Boolean, default: true}
  })
</script>


<template>
<div :id="modalId" class="modal fade">
  <div class="modal-dialog modal-dialog-centered modal-xl">
    <div class="modal-content">
      <div v-if="showClose" class="modal-header d-flex justify-content-end">
        <button class="btn" title="close modal fs-5" data-bs-dismiss="modal"><i class="mdi mdi-close"></i></button>
      </div>
      <div class="modal-body">
        <slot>
          this is a modal
        </slot>
      </div>
    </div>
  </div>
</div>
</template>


<style lang="scss" scoped>

</style>