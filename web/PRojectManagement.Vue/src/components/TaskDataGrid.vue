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
        @row-removing="onRowRemoving"
        @row-updated="onRowUpdated"
        @initialized="onInitialized"
      >
        <DxToolbar  :visible="true">
          <DxItem name="groupPanel" />
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

        <DxDataGridColumn data-field="name" caption="Наименование задачи">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="authorId" :visible="false" caption="Создал задачу">
        </DxDataGridColumn>
        <DxDataGridColumn data-field="author" caption="Создал задачу">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="executorId" :visible="false" caption="Исполнитель">
        </DxDataGridColumn>
        <DxDataGridColumn data-field="executor" caption="Исполнитель">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="projectId" :visible="false" caption="Проект">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="project" caption="Проект">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="status" caption="Статус" :calculate-display-value="displayStatus">
          <DxLookup
            :data-source="itemsStatus"
            display-expr="name"
            value-expr="id"
          />
        </DxDataGridColumn>

        <DxDataGridColumn data-field="comment" caption="Комментарий">
        </DxDataGridColumn>
        <DxDataGridColumn data-field="priority" caption="Приоритет" :calculate-display-value="displayPriority">
          <DxLookup
            v-model="statusValue"
            :items="itemsPriority"
            display-expr="name"
            value-expr="id"
          />
        </DxDataGridColumn>


        <DxFilterRow :visible="true"/>
        

        

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
            :max-width="600"
            :height="525"
            title="Добавить/редактировать сотрудника"
          />
          <DxForm
            label-location="top"
          >
              
            <DxItemForm
              :col-count="2"
              :col-span="2"
              item-type="group"
            >
              <DxItemForm  data-field="id" :visible="false" v-if="false"/>
              <DxItemForm  data-field="name" caption="Наименование задачи"/>
              <DxItemForm  
                data-field='authorId'
                editor-type='dxLookup'
                caption='Создал задачу'
                :editor-options="{
                  dataSource: employees,
                  items: employees,
                  displayExpr: customDisplayExpr,
                  valueExpr: 'id'
                }"
              /> 
              <DxItemForm  
                data-field="executorId" 
                caption="Исполнитель"
                editor-type='dxLookup'
                :editor-options="{
                  dataSource: employees,
                  displayExpr: customDisplayExpr,
                  valueExpr: 'id'
                }"       
              />
              
              <DxItemForm  
                data-field="projectId"
                caption="Проект"
                :disabled="true"
                editor-type='dxLookup'
                :editor-options="{
                  dataSource: projects,
                  items: projects,
                  displayExpr: 'name',
                  valueExpr: 'id'
                }"       
              />
              <DxItemForm  
                data-field="status"
                caption="Статус"
                editor-type='dxLookup'
                :editor-options="{
                  dataSource: itemsStatus,
                  displayExpr: 'name',
                  valueExpr: 'id'
                }"    
              />
              <DxItemForm  
                data-field="priority"
                caption="Приоритет"
                editor-type='dxLookup'
                :editor-options="{
                  dataSource: itemsPriority,
                  displayExpr: 'name',
                  valueExpr: 'id'
                }"    
              />
              <DxItemForm  
                data-field="comment" 
                caption="Комментарий"
                :col-span="2" 
                editor-type="dxTextBox"
                :editor-options="{
                  height: '40px'
                }"    
              >
              </DxItemForm>

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
    DxCustomRule,
    DxHeaderFilter
    
  } from 'devextreme-vue/data-grid';
  import { DxLookup, DxDropDownOptions } from 'devextreme-vue/lookup';
  import { DxButton } from 'devextreme-vue/button';
  import {defineComponent, onMounted, ref, computed, onUpdated, reactive} from 'vue';
  import { DxItem as DxItemForm } from 'devextreme-vue/form'
  import { DxTextArea } from 'devextreme-vue/text-area';
  import axios from 'axios';


  const props = defineProps({
    dataSourceg: {
        type: Array
    }
  })


  const dataSource = computed(() => props.dataSourceg)
  const selectedRowIndex = ref(-1)
  const dataGridRef = ref()
  const grid = computed(() => dataGridRef.value.instance);
  const statusValue = ref(null)
  const employees = ref([])
  const projects = ref([])
  const itemsStatus = ref([
    { id: 0, name: "ToDo" },
    { id: 1, name: "InProgress" },
    { id: 2, name: "Done" },
  ]);
  const itemsPriority = ref([
    { id: 0, name: "Низкий" },
    { id: 1, name: "Средний" },
    { id: 2, name: "Высокий" },
  ]);
  function customDisplayExpr(item:any){
    if(item != undefined){
      return `${item.firstName} ${item.lastName}`
    }
    return ""
  }


  


  function displayStatus(rowData:any){
    return itemsStatus.value.find(item => item.id === rowData.status)?.name
  }
  function displayPriority(rowData:any){
    return itemsPriority.value.find(item => item.id === rowData.priority)?.name
  }

  async function selectedChanged(e: any) {
    console.log(e);
    await GetEmployees()
    await GetProjects()
    selectedRowIndex.value = e.component.getRowIndexByKey(e.selectedRowKeys[0]); 
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

  async function onRowRemoving(e:any) {
    console.log("onRowRemoving");
    console.log(e.data["id"]);
    const id = e.data["id"]
    await axios.delete(`https://localhost:7104/api/Task/${id}`)
  }

  async function onRowUpdated(e:any) {
    console.log("onRowUpdated");
    console.log(e);
    await axios.put(`https://localhost:7104/api/Task`, e.data);
  }

  async function onInitialized(e:any) {
    console.log("onInitialized");
    console.log(e);
  }

  async function GetEmployees(){
        const res = await axios.get(
          `https://localhost:7104/api/Employee`
        )
        employees.value = res.data
  }

  async function GetProjects(){
        const res = await axios.get(
          `https://localhost:7104/api/Project`
        )
        projects.value = res.data
  }



  defineComponent({
    name:"Tasks",
  })
  </script>