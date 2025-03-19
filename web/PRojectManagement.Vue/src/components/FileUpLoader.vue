<template>
  <div>
    <div
      class="border-2 border-dashed border-gray-300 p-6 rounded-lg text-center cursor-pointer"
      :class="{ 'bg-gray-100': isDragging }"
      @dragover.prevent="isDragging = true"
      @dragleave="isDragging = false"
      @drop="handleDrop"
    >
      <p class="text-gray-500">Перетащите файлы сюда или</p>
      <label for="fileInput" class="text-blue-500 underline cursor-pointer">
        выберите файлы
      </label>
      <input id="fileInput" type="file" multiple class="hidden" @change="handleFileInput" />
    </div>

    <ul v-if="files.length" class="mt-4 space-y-2">
      <li v-for="file in files" :key="file.name" class="text-sm text-gray-700">
        📄 {{ file.name }} ({{ (file.size / 1024).toFixed(1) }} KB)
      </li>
    </ul>

    <button @click="uploadFiles" class="mt-4 bg-blue-500 text-white px-4 py-2 rounded">
      Загрузить файлы
    </button>

    <p v-if="uploadStatus" class="mt-2 text-sm text-gray-600">{{ uploadStatus }}</p>
  </div>
  <FileDataGrid :data-sourceg="dataSource" :project-id="props.projectId"/>
</template>

<script setup lang="ts">
import { computed, ref, toRef } from "vue";
import axios from "axios";
import FileDataGrid from "./FileDataGrid.vue";
import { compileScript } from "vue/compiler-sfc";

const emit = defineEmits<{
        (event: "return-dataSource", DataSorce: Array<Object>): void;
}>();

const props = defineProps({
    dataSource:{
        type: Array
    },
    projectId:{
        type: Number
    }
});

const files = ref<File[]>([]);
const isDragging = ref(false);
const uploadStatus = ref<string | null>(null);
const dataSource = computed(() => props.dataSource);

const handleDrop = (event: DragEvent) => {
  event.preventDefault();
  isDragging.value = false;
  if (event.dataTransfer?.files.length) {
    addFiles(event.dataTransfer.files);
  }
};

const addFiles = (fileList: FileList) => {
  for (const file of fileList) {
    files.value.push(file);
  }
};

const handleFileInput = async (event: Event) => {
  const input = event.target as HTMLInputElement;
  console.log(dataSource.value)
  console.log(input.files)
  if (input.files?.length) {
    addFiles(input.files);
  }
};

const uploadFiles = async () => {
   

    if (files.value.length === 0) {
        uploadStatus.value = "Выберите файлы перед загрузкой!";
        return;
    }

    const formData = new FormData();
    files.value.forEach(file => formData.append("files", file));

    try {
        const response = await axios.post(`https://localhost:7104/api/File/${props.projectId}`, formData, {
        headers: { "Content-Type": "multipart/form-data" }
        });
        emit('return-dataSource', await GetFiles())
        uploadStatus.value = "Файлы успешно загружены!";
        files.value = [];
        console.log(response.data);
    } catch (error) {
        uploadStatus.value = "Ошибка загрузки!";
        console.error(error);
    }
};


async function GetFiles(){
    const res = await axios.get(`https://localhost:7104/api/File/${props.projectId}`)
    return res.data
}

</script>