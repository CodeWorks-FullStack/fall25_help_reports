<script setup>
import { restaurantsService } from '@/services/RestaurantsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute()

onMounted(getRestaurantById)

async function getRestaurantById() {
  try {
    await restaurantsService.getRestaurantById(route.params.restaurantId)
  } catch (error) {
    Pop.error(error)
    logger.error('COULD NOT GET RESTAURANT BY ID', error)
  }
}
</script>


<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-12">

      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped></style>