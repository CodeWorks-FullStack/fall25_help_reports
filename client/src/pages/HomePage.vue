<script setup>
import { restaurantsService } from '@/services/RestaurantsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';

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
      <div class="col-md-4">

      </div>
    </div>
  </div>
</template>

<style scoped lang="scss"></style>
