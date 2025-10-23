<script setup>
import { Report } from '@/models/Report.js';

defineProps({report:{type: Report, required: true}})
</script>


<template>
<div class="card shadow">
  <div class="card-header">

    <div class="fs-4 text-success text-center d-flex">
      <div class="flex-grow-1">
        <i class="mdi mdi-format-quote-close"></i>{{ report.title }}<i class="mdi mdi-format-quote-close"></i>
      </div>
      <div class="d-flex  flex-column align-items-center justify-content-end">
        <div class="text-dark">
          {{ report.creator.name }}
           <img class="profile-img" :src="report.creator.picture" alt="a picture of somebody"> 
        </div>
           <div class="text-secondary">
        {{ report.createdAt.toLocaleDateString('en-us', {month:'short', day:'2-digit', year:'numeric'}) }}
      </div>
      </div>
    </div>

  </div>

  <div class="card-body">
    "{{ report.body }}"
  </div>
</div>
</template>


<style lang="scss" scoped>
.profile-img{
  height: 40px;
  width: 40px;
  border-radius: 100px;
  object-fit: cover;
  object-position: center;
}
</style>