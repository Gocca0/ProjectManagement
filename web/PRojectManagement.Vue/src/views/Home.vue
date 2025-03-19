<template>
    <div class="bg-blue-500 text-white p-4">
      <p>Home</p>
    </div>
  </template>
  
  <script lang="ts">
  import { RouterLink, RouterView } from 'vue-router'
  import {defineComponent} from 'vue'
  
  export default defineComponent({
    name:"Home",
  })
  </script>