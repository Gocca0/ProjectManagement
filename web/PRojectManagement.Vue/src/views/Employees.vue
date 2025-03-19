<template>
    <div id="app-container" style="margin-top: 4px !important;">
      <DxDataGrid 
        id="dataGrid"
        ref="dataGridRef"
        :dataSource = dataSource
        :selection="{ mode: 'single' }"
        :show-borders="true"
        key-expr="id"
        @selection-changed="selectedChanged"
        @row-inserted="onRowInserted"
        @row-inserting="onRowInserting"
        @row-removing="onRowRemoving"
        @row-updated="onRowUpdated"
      >
        <DxToolbar  :visible="true">
          <DxItem name="groupPanel" />
          <DxItem template="addBtn-template"/> 
          <DxItem 
            template="editBtn-template"
            :visible="selectedRowIndex !== -1"
          />
          <DxItem 
            template="deleteBtn-template"
            :visible="selectedRowIndex !== -1"
          />
        </DxToolbar>

        <DxDataGridColumn 
          data-field="id" 
          :visible="false"
          sort-order="asc"
          :allow-editing="false"
        >
        </DxDataGridColumn>

        <DxDataGridColumn data-field="firstName" caption="Имя">
          <DxRequiredRule message="Имя обязательно"/>
        </DxDataGridColumn>

        <DxDataGridColumn data-field="lastName" caption="Фамилия">
          <DxRequiredRule message="Фамилия обязательно"/>
        </DxDataGridColumn>

        <DxDataGridColumn data-field="email">
          <DxRequiredRule message="Email обязательно"/>]
          <DxCustomRule :validation-callback="emailValidation" message="Некорректный email"/>
        </DxDataGridColumn>

        <DxFilterRow :visible="true"/>

        <template #addBtn-template>
          <DxButton
            text="Добавить"
            @click="addRow"
            icon="add"
          >
          </DxButton>
        </template>

        <template #deleteBtn-template>
          <DxButton
            text="Удалить"
            @click="deleteRow"
            icon="clear"
          >
          </DxButton>
        </template>
        <template #editBtn-template>
          <DxButton
            text="Редактировать"
            @click="editRow"
            icon="edit"
          >
          </DxButton>
        </template>


        <DxEditing
            mode="popup"           
        >
          <DxPopup
            :show-title="true"
            :width="400"
            :height="525"
            title="Добавить/редактировать сотрудника"
          />
          <DxForm>
              
            <DxItemForm
              :col-count="1"
              :col-span="2"
              item-type="group"
            >
              <DxItemForm  data-field="id" :visible="false" v-if="false"/>
              <DxItemForm  data-field="firstName"/>
              <DxItemForm  data-field="lastName"/>
              <DxItemForm  data-field="email"/>
            </DxItemForm>
          </DxForm>   
        </DxEditing>
      </DxDataGrid>
    </div>
  </template>
  
  <script setup lang="ts">
  import { 
    DxDataGrid, 
    DxColumn as DxDataGridColumn,
    DxFilterRow,
    DxToolbar,
    DxItem,
    DxRequiredRule,
    DxEditing,
    DxPopup,
    DxForm,
    DxCustomRule
    
  } from 'devextreme-vue/data-grid';

  import { DxButton } from 'devextreme-vue/button';
  import {defineComponent, onMounted, ref, computed, onUpdated} from 'vue';
  import { DxItem as DxItemForm } from 'devextreme-vue/form'
  import axios from 'axios';
  
  const dataSource = ref([])
  const selectedRowIndex = ref(-1)
  const dataGridRef = ref()
  const grid = computed(() => dataGridRef.value.instance);

 
  const emailValidation = (e:any) => {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailPattern.test(e.value);
  };


  onMounted(()=>{
    GetEmployees()
  })


  function selectedChanged(e: any) {
    console.log(e);
    selectedRowIndex.value = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
  }

  function addRow(e:any) {
    grid.value.addRow()
    grid.value.deselectAll();
  }

  function deleteRow(e:any) {
    grid.value.deleteRow(selectedRowIndex.value);
    grid.value.deselectAll();
  }

  function editRow(e:any) {
    console.log(e);
    grid.value.editRow(selectedRowIndex.value);
    grid.value.deselectAll();
  }

  async function onRowInserting(e:any) {
    console.log("onRowInserting");
    console.log(e.data);
    await axios.post(
          `https://localhost:7104/api/Employee`,
          e.data
        )
  }

  async function onRowInserted(e:any) {
    console.log("OnRowInsterted");
    console.log(e.data);
    await GetEmployees()
  }

  async function onRowRemoving(e:any) {
    console.log("onRowRemoving");
    console.log(e.data["id"]);
    const id = e.data["id"]
    await axios.delete(`https://localhost:7104/api/Employee/${id}`)
  }

  async function onRowUpdated(e:any) {
    console.log("onRowUpdated");
    console.log(e);
    await axios.put(`https://localhost:7104/api/Employee`, e.data);
    await GetEmployees();
  }
 

  
  async function GetEmployees(){
        const res = await axios.get(
          `https://localhost:7104/api/Employee`
        )
        dataSource.value = res.data 
  }


  defineComponent({
    name:"Employees",
  })
  </script>