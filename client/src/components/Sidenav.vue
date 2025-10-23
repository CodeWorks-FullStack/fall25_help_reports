<script setup>
import Login from './Login.vue';


</script>


<template>
  <div class="d-flex flex-column gap-4 p-3">
    <RouterLink :to="{ name: 'Home' }" class="">
      <img src="@/assets/img/helpLogo.png" alt="help reports logo" class="img-fluid">
    </RouterLink>
      <button data-bs-toggle="modal" data-bs-target="#create-report" class="btn btn-mushy-pea rounded-0 p-3 fs-4 fw-bold">Report!</button>
    <div>
      <Login/>
    </div>
  </div>
</template>


<style lang="scss" scoped>

</style>