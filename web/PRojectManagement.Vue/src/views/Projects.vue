<template>
    <div id="app-container" style="margin-top: 4px !important;">
      <DxDataGrid 
        id="dataGrid"
        ref="dataGridRef"
        :dataSource = dataSource
        :selection="{ mode: 'single' }"
        :show-borders="true"
        key-expr="id"
        @initialized="onInitialized"
        @selection-changed="selectedChanged"
        @row-inserted="onRowInserted"
        @row-inserting="onRowInserting"
        @row-removing="onRowRemoving"
        @row-updated="onRowUpdated"
        @init-new-row="onInitRow"
      >
        <DxToolbar  :visible="true">
          <DxItem name="groupPanel" />
          <DxItem template="addBtn-template"/> 
          <DxItem template="clearAllFilterBtn-template"/> 
          <DxItem 
            template="editBtn-template"
            :visible="selectedRowIndex !== -1"
          />
          <DxItem 
            template="detailsBtn-template"
            :visible="selectedRowIndex !== -1"
          />
          <DxItem 
            template="deleteBtn-template"
            :visible="selectedRowIndex !== -1"
          />   
        </DxToolbar>

        <DxDataGridColumn data-field="id" :visible="false">

        </DxDataGridColumn>

        <DxDataGridColumn data-field="name" caption="Нименование проекта">
          <DxRequiredRule/>
        </DxDataGridColumn>

        <DxDataGridColumn data-field="customerCompany" caption="Компания-заказчик">
          <DxRequiredRule/>
        </DxDataGridColumn>
        <DxDataGridColumn data-field="executorCompany" caption="Компания-Исполнитель">
          <DxRequiredRule/>
        </DxDataGridColumn>

        <DxDataGridColumn data-field="startDate" data-type="date" format="dd.MM.yyyy">
          <DxRequiredRule/>
        </DxDataGridColumn>
        <DxDataGridColumn data-field="endDate" data-type="date" format="dd.MM.yyyy">
          <DxRequiredRule/>
        </DxDataGridColumn>



        <DxDataGridColumn data-field="managerId" :visible="false">
          <DxRequiredRule/>
        </DxDataGridColumn>
        <DxDataGridColumn data-field="manager">
        </DxDataGridColumn>





        <DxFilterRow :visible="true"/>
        <DxHeaderFilter :visible="true"/>


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
        <template #detailsBtn-template>
          <DxButton
            text="Детальная информация"
            @click="detailsProject"
            icon="info"
          >
          </DxButton>
        </template>
        <template #clearAllFilterBtn-template>
          <DxButton
            text="Очистить фильтры и сортировку"
            @click="clearFilter"
            icon="clear"
          >
          </DxButton>
        </template>


        <DxEditing
            mode="popup"           
        >
          <DxPopup
            :show-title="true"
            :width="400"
            :height="600"
            title="Добавить/редактировать проект"
          />
          <DxForm
            label-location="top"
          >
              
            <DxItemForm
              :col-count="1"
              :col-span="2"
              item-type="group"
            >
              <DxItemForm  data-field="id" :visible="false" v-if="false"/>
              <DxItemForm data-field="name"/>
              <DxItemForm data-field="customerCompany"/>
              <DxItemForm data-field="executorCompany"/>
              <DxItemForm data-field="startDate" data-type="date" format="dd.MM.yyyy"/>
              <DxItemForm data-field="endDate" data-type="date" format="dd.MM.yyyy"/>  
              <DxItemForm data-field="managerId">
                <DxLookup
                  :data-source="dataSourceEmployees"
                  :value="selectedId"
                  :items="dataSourceEmployees"
                  :display-expr="customDisplayExpr"
                  value-expr="id"
                  @value-changed="onValueChanged"
                >
                  <DxDropDownOptions
                    :hide-on-outside-click="true"
                    :show-title="false"
                  />
                </DxLookup>
              </DxItemForm>
            </DxItemForm>
          </DxForm>   
        </DxEditing>
      </DxDataGrid>
    </div>
    <DetailsInfoPopup 
      :is-popup-visible="isPopupVisible" 
      :data-selected-row="selectedRowData"
      @hidden-popup="hiddenPopup"
    />
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
    DxCustomRule,
    DxHeaderFilter
    
  } from 'devextreme-vue/data-grid';
  import { DxLookup, DxDropDownOptions } from 'devextreme-vue/lookup';
  import { DxButton } from 'devextreme-vue/button';
  import { DxPopup as DefaultPopup } from 'devextreme-vue/popup';
  import {defineComponent, onMounted, ref, computed, onUpdated, reactive} from 'vue';
  import { DxItem as DxItemForm } from 'devextreme-vue/form'
  import  DetailsInfoPopup   from '@/components/DetailsInfoPopup.vue'
  import axios from 'axios';

  const isPopupVisible = ref(false)
  const dataSource = ref([])
  const selectedId = ref(-1)
  const selectedRowData = ref({})
  const dataSourceEmployees = ref([])
  const selectedRowIndex = ref(-1)
  const dataGridRef = ref()
  const grid = computed(() => dataGridRef.value.instance);
  const propsgrid = computed(() => dataGridRef.value.instance);

  function customDisplayExpr(item:any){
    if(item != undefined){
      return `${item.firstName} ${item.lastName}`
    }
    return ""
  }

 

  onMounted(()=>{
    GetProjects()
  })


  function selectedChanged(e: any) {
    console.log("selectedChanged")
    console.log(e.selectedRowsData[0]);
    console.log(dataGridRef);
    if (e.selectedRowsData[0] != undefined){
      selectedRowData.value = e.selectedRowsData[0]
      console.log(selectedRowData.value)
      selectedId.value = e.selectedRowsData[0]["managerId"]
    }
    selectedRowIndex.value = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
  }

  function addRow(e:any) {
    grid.value.addRow()
    grid.value.deselectAll();
    selectedId.value = -1
  }

  function deleteRow(e:any) {
    grid.value.deleteRow(selectedRowIndex.value);
    grid.value.deselectAll();
    selectedId.value = -1
  }

  function editRow(e:any) {
    console.log(e);
    grid.value.editRow(selectedRowIndex.value);
    grid.value.deselectAll();
  }

  function clearFilter(){
    grid.value.clearFilter()
    grid.value.clearSorting()
  }

  async function onRowInserting(e:any) {
    console.log("onRowInserting");
    e.data["managerId"] = selectedId.value
    console.log(e.data);
    await axios.post(`https://localhost:7104/api/Project`, e.data)
  }

  async function onRowInserted(e:any) {
    console.log("OnRowInsterted");
    console.log(e.data);
    await GetProjects()
  }

  async function onRowRemoving(e:any) {
    console.log("onRowRemoving");
    console.log(e.data["id"]);
    const id = e.data["id"]
    await axios.delete(`https://localhost:7104/api/Project/${id}`)
  }

  async function onRowUpdated(e:any) {
    console.log("onRowUpdated");
    console.log(e);
    await axios.put(`https://localhost:7104/api/Project`, e.data);
    await GetProjects();
  }
  async function onInitialized(e:any) {
    console.log("onInitialized");
    console.log(e);
    
    isPopupVisible.value = false;
    await GetProjects()
    await GetEmployees();
  }
  async function onInitRow(e:any) {
    console.log("onInitRow");
    console.log(e);
  }

  async function onValueChanged(e:any){
    console.log(e);
    selectedId.value = e.value
  }

  async function detailsProject() {
    isPopupVisible.value = !isPopupVisible.value
    console.log(isPopupVisible.value)
    
  }

  function hiddenPopup(close:any){
    isPopupVisible.value = close
  }

  

  
  async function GetEmployees(){
        const res = await axios.get(
          `https://localhost:7104/api/Employee`
        )
        dataSourceEmployees.value = res.data 
        console.log(dataSourceEmployees.value)
  }
  async function GetProjects(){
        const res = await axios.get(
          `https://localhost:7104/api/Project/withEmployeesAndTasks`
        )
        dataSource.value = res.data 
        console.log(dataSource.value)
  }

  defineComponent({
    name:"Projects",
  })
  </script>