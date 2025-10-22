<script setup>
import { Restaurant } from '@/models/Restaurant.js';


defineProps({
  restaurant: { type: Restaurant, required: true }
})

</script>


<template>
  <div class="shadow">
    <img :src="restaurant.imgUrl" :alt="`A picture of the ${restaurant.name} restaurant`" class="restaurant-img">
    <div class="py-2 px-3 bg-light">
      <b class="text-mushy-pea baloo-da-2-font">{{ restaurant.name }}</b>
      <p>{{ restaurant.description }}</p>
      <div class="d-flex justify-content-between">
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
    </div>
  </div>
</template>


<style lang="scss" scoped>
.restaurant-img {
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
}
</style>