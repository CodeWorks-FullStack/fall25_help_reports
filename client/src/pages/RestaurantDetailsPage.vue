<script setup>
import { AppState } from '@/AppState.js';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const restaurant = computed(() => AppState.restaurant)
const account = computed(() => AppState.account)

const route = useRoute()
const router = useRouter()

onMounted(getRestaurantById)

async function getRestaurantById() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  } catch (error) {
    Pop.error(error)
    logger.error('COULD NOT GET RESTAURANT BY ID', error)
  }
}

async function deleteRestaurant() {
  const confirmed = await Pop.confirm(`Are you sure you want to delete ${restaurant.value.name}?`)

  if (!confirmed) return

  try {
    await restaurantsService.deleteRestaurant(route.params.restaurantId)
    router.push({ name: 'Home' })
  } catch (error) {
    Pop.error(error)
    logger.error('COULD NOT DELETE RESTAURANT', error)
  }
}
</script>


<template>
  <div v-if="restaurant" class="container-fluid">
    <div class="row">
      <div class="col-12">
        <div class="baloo-da-2-font d-flex align-items-baseline justify-content-between">
          <div>
            <h1 class="fs-2 text-mushy-pea">
              <b>{{ restaurant.name }}</b>
            </h1>
          </div>
          <div class="fs-2 bg-danger text-light px-3">
            <span class="mdi mdi-close-circle"></span>
            <b>Currently Shutdown</b>
          </div>
        </div>
        <div class="bg-light shadow">
          <img :src="restaurant.imgUrl" :alt="restaurant.name" class="restaurant-img">
          <div class="px-3 py-2">
            <p class="mb-5">{{ restaurant.description }}</p>
            <div class="d-flex align-items-center justify-content-between">
              <div class="d-flex gap-5">
                <div class="d-flex align-items-center gap-1">
                  <span class="mdi mdi-account-multiple fs-1 text-mushy-pea"></span>
                  <b>{{ restaurant.visits }}</b>
                  <span>recent visits</span>
                </div>
                <div class="d-flex align-items-center gap-1">
                  <span class="mdi mdi-file-multiple fs-1 text-mushy-pea"></span>
                  <b>{{ restaurant.reportCount }}</b>
                  <span>reports</span>
                </div>
              </div>
              <div v-if="account?.id == restaurant.creatorId" class="d-flex gap-5">
                <button class="btn btn-success fs-4" type="button">
                  <span class="mdi mdi-door-open"></span>
                  Re-Open
                </button>
                <button @click="deleteRestaurant()" class="btn btn-danger fs-4" type="button">
                  <span class="mdi mdi-delete-forever"></span>
                  Delete
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div v-else class="container-fluid">
    <div class="row">
      <div class="col-12">
        <marquee direction="right" scrollamount="25" class="d-flex gap-5">
          <span class="mdi mdi-rodent fs-1"></span>
          <span class="mdi mdi-rodent fs-1 text-mushy-pea"></span>
          <span class="mdi mdi-rodent fs-1 text-indigo"></span>
        </marquee>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.restaurant-img {
  width: 100%;
  height: 400px;
  object-fit: cover;
  object-position: center;
}
</style>