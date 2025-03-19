<template>
  <header>
    <NavMenu :msg='title'/>
  </header>
  <router-view></router-view>
</template>

<script lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import NavMenu from './components/NavMenu.vue'
import {defineComponent, ref} from 'vue'

export default defineComponent({
  data(){
    return{
      title:"Управление проектами"
    }
  },
  name:"App",
  components: {NavMenu}
})
</script>


