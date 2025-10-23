<script setup>
import { AppState } from '@/AppState.js';
import { reportsService } from '@/services/ReportsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';

const restaurants = computed(()=> AppState.restaurants)
const selectedRestaurant = computed(()=> AppState.restaurants?.find(r=> r.id == reportData.value.restaurantId))

const reportData = ref({
  title: '',
  body: '',
  imgUrl: '',
  score: 1,
  restaurantId: 0
})

async function submitReport(){
  try {
    logger.log('📄',reportData.value)
    await reportsService.createReport(reportData.value)
  } catch (error) {
    logger.error(error)
    Pop.error(error, 'Could not submit form')
  } finally
  {
    clearAndCloseForm()
  }
}

function clearAndCloseForm(){
  reportData.value = {
  title: '',
  body: '',
  imgUrl: '',
  score: 1,
  restaurantId: 0
}
Modal.getOrCreateInstance('#create-report').hide()
}

</script>


<template>
<form @submit.prevent="submitReport" class="row">
  <div class="fs-4 text-success fw-bold">What do you want to say?</div>
  <div class="mb-3 col-12">
    <label for="restaurant-id fw-bold">Select Restaurant</label>
    <div class="row">
      <div class="col-6">
        <img class="p-1 bg-mushy-pea-subtle rounded preview-img" :src="selectedRestaurant?.imgUrl ?? 'https://plus.unsplash.com/premium_photo-1738740636827-f37586440c67?ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fGdyZWVuJTIwb2dyZXxlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&q=60&w=500'" alt="">
      </div>
      <div class="col-6">
        <select v-model="reportData.restaurantId" class="form-control" name="restaurant-id" id="restaurant-id" required>
          <option v-for="restaurant in restaurants" :key="`dropdown-${restaurant.id}`" :value="restaurant.id">{{ restaurant.name }}</option>
        </select>

      </div>
    </div>
  </div>
  <div class="mb-3 col-12">
    <label for="report-title" class="fw-bold">Title for report</label>
    <input v-model="reportData.title" class="form-control" name="report-title" id="report-title" type="text" required maxlength="500">
  </div>
  <div class="mb-3 col-12">
    <label for="report-body" class="fw-bold">Report Details</label>
    <textarea v-model="reportData.body" maxlength="2000" class="form-control" name="report-body" id="report-body" rows="10"></textarea>
  </div>
  <div class="mb-3 col-6">
    <label for="report-score fw-bold">
      Score it
      <span v-for="n in reportData.score" :key="`score-${n}`" class="mdi mdi-rodent fs-3"></span>
    </label>
    <input v-model.number="reportData.score" type="range" class="form-range" max="5" min="1" required>
  </div>
  <div class="mb-3 d-flex justify-content-end">
    <button class="btn" type="button">cancel</button>
    <button class="btn btn-mushy-pea" title="submit report"><i class="mdi mdi-file-document-arrow-right"></i> Submit Report</button>
  </div>

</form>
</template>


<style lang="scss" scoped>
.preview-img{
  height: 100px;
  width: 100%;
  object-fit: cover;
  object-position: center;
}
</style>