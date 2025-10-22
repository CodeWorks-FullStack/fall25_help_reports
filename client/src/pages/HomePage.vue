<script setup>
import { AppState } from '@/AppState.js';
import RestaurantListing from '@/components/RestaurantListing.vue';
import { restaurantsService } from '@/services/RestaurantsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const restaurants = computed(() => AppState.restaurants)

onMounted(getRestaurants)

async function getRestaurants() {
  try {
    await restaurantsService.getRestaurants()
  } catch (error) {
    Pop.error(error)
    logger.error('COULD NOT GET RESTAURANTS', error)
  }
}

</script>

<template>
  <div class="container-fluid">
    <div class="row">
      <div v-for="restaurant in restaurants" :key="'restaurant-list-item-' + restaurant.id" class="col-md-4">
        <RestaurantListing :restaurant="restaurant" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>
