<template>
  <nav class="bg-gray-800 text-white p-4">
    <div class="container mx-auto flex justify-between items-center">
    
      <div class="text-xl font-bold">
        <a href="/" style="color: white !important;">{{ msg }}</a>
      </div>
      <ul
        class="md:flex space-x-4 hidden text-base"
        :class="'block mt-4'"
      >
        <li v-for="item in menuItems" :key="item.path">
            <a
            :href="item.path"
            class="hover:bg-gray-700  text-green-500 px-3 py-2 rounded transition"
          >
            {{ item.name }}
          </a>
            
        </li>
      </ul>
    </div>
  </nav>
</template>

<script lang="ts">
import { RouterLink} from 'vue-router'
import { ref,defineComponent } from "vue";

const menuItems = ref([
  { name: "Сотрудники", path: "/employees" },
  { name: "Проекты", path: "/projects" },
  { name: "Задачи", path: "/tasks" },
]);

export default defineComponent({
    props:{
        msg: String
    },
    data(){
        return{
            menuItems: [
            { name: "Сотрудники", path: "/employees" },
            { name: "Проекты", path: "/projects" },
            { name: "Задачи", path: "/tasks" },
            ]
        }
    }

    
})

</script>