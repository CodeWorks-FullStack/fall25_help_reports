<script setup>
import { Profile } from '@/models/Profile.js';

defineProps({
  profile: { type: Profile, required: true }
})
</script>


<template>
  <img :src="profile.picture" :alt="profile.name" :title="profile.name">
</template>


<style lang="scss" scoped>
img {
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
}
</style>