<script setup>
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';
import Sidenav from './components/Sidenav.vue';
import ModalWrapper from './components/ModalWrapper.vue';
import CreateReportForm from './components/CreateReportForm.vue';

</script>

<template>
  <div class="container-fluid">
    <section class="row">
      <div class="col-md-2 p-0">

        <Navbar class="d-flex d-md-none" />
         <Sidenav class="d-none d-md-flex"/>
      </div>
      <main class="col-md-10">
        <RouterView />
      </main>
    </section>
  </div>
  <ModalWrapper modalId="create-report" :showClose="true">
    <CreateReportForm/>
  </ModalWrapper>
</template>

<style lang="scss">


</style>
