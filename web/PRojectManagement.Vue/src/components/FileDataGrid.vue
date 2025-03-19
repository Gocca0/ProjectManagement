<template>
    <DxDataGrid 
        id="datagridEmployeesInProject"
        ref="dataGridRef"
        :dataSource = props.dataSourceg
        :selection="{ mode: 'single' }"
        :show-borders="true"
        key-expr="id"
        @selection-changed="selectedChanged"
        @row-removing="onRowRemoving"
        :height="342"
    >
        <DxToolbar  :visible="true">
          <DxItem name="groupPanel" />downloadBtn-template
          <DxItem 
            template="downloadBtn-template"
            :visible="selectedRowIndex !== -1"
          />
          <DxItem 
            template="deleteBtn-template"
            :visible="selectedRowIndex !== -1"
          />
          <DxItem 
            template="infoBtn-template"
            :visible="false"
          />
        </DxToolbar>

        <DxDataGridColumn 
          data-field="id" 
          :visible="false"
          sort-order="asc"
          :allow-editing="false"
        >
        </DxDataGridColumn>

        <DxDataGridColumn data-field="name" caption="Имя файла">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="projectId" :visible="false">
        </DxDataGridColumn>

        <DxPager
          :visible="true"
          display-mode="full"
          :show-navigation-buttons="true"
        />
        <DxPaging :page-size="5"/>
        <DxFilterRow :visible="true"/>

        <template #deleteBtn-template>
            <DxButton
                text="Удалить"
                @click="deleteRow"
                icon="clear"
            >
            </DxButton>
        </template>
        <template #downloadBtn-template>
            <DxButton
                text="Скачать файл"
                @click="downloadFile"
                icon="download"
            >
            </DxButton>
        </template>
        <template #infoBtn-template>
            <DxButton
                text="info"
                @click="InfoConsole"
                icon="clear"
            >
            </DxButton>
        </template>

    </DxDataGrid>
</template>

<script setup lang="ts">
  import { 
    DxDataGrid, 
    DxColumn as DxDataGridColumn,
    DxFilterRow,
    DxToolbar,
    DxItem,
    DxEditing,
    DxPopup,
    DxForm,
    DxPager,
    DxPaging
    
  } from 'devextreme-vue/data-grid';
  import { DxLookup, DxDropDownOptions } from 'devextreme-vue/lookup';
  import { DxButton } from 'devextreme-vue/button';
  import {defineComponent, onMounted, ref, computed, onUpdated, reactive, toRef} from 'vue';
  import { DxItem as DxItemForm } from 'devextreme-vue/form'
  import axios from 'axios';


  const selectedRowIndex = ref(-1)
  const dataGridRef = ref()
  const grid = computed(() => dataGridRef.value.instance);
  const fileId = ref("")


  const props = defineProps({
    dataSourceg: {
        type: Array
    },
    projectId:{
        type: Number
    }
  })

  
  function deleteRow(e:any) {
    grid.value.deleteRow(selectedRowIndex.value);
    grid.value.deselectAll();
  }

  function selectedChanged(e: any) {
    console.log(e);
    selectedRowIndex.value = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
    fileId.value = e.selectedRowsData[0]?.id
  }

  async function onRowRemoving (e:any) {
    await axios.delete(`https://localhost:7104/api/File/${props.projectId}/deletefile/${e.key}`)
  }

  async function downloadFile() {
    await axios.get(`https://localhost:7104/api/File/Download/${fileId.value}`)
  }
  async function InfoConsole() {
    console.log(props.dataSourceg)  
  }



  </script>