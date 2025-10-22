<script setup>
import { Restaurant } from '@/models/Restaurant.js';
import ProfilePicture from './ProfilePicture.vue';


defineProps({
  restaurant: { type: Restaurant, required: true }
})

</script>


<template>
  <RouterLink :to="{ name: 'Restaurant Details', params: { restaurantId: restaurant.id } }"
    :title="`Visit the ${restaurant.name} page`">

    <div class="shadow">
      <div class="position-relative">
        <img :src="restaurant.imgUrl" :alt="`A picture of the ${restaurant.name} restaurant`" class="restaurant-img"
          :class="{ shutdown: restaurant.isShutdown }">
        <ProfilePicture :profile="restaurant.owner" height="100"
          class="position-absolute bottom-0 end-0 hover-profile border border-3 border-mushy-pea" />
      </div>
      <div class="py-2 px-3 bg-light">
        <b class="baloo-da-2-font" :class="restaurant.isShutdown ? 'text-danger' : 'text-mushy-pea'">
          {{ restaurant.name }}
        </b>
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
  </RouterLink>
</template>


<style lang="scss" scoped>
a {
  text-decoration: none;
  color: unset;
}

.restaurant-img {
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
}

.hover-profile {
  opacity: 0;
  transition: opacity 300ms ease;
}

.shadow:hover .hover-profile {
  opacity: 1;
}

.shutdown {
  filter: grayscale(1);
}
</style>