<template>
    <DxDataGrid 
        id="datagridEmployeesInProject"
        ref="dataGridRef"
        :dataSource = props.dataSourceg
        :selection="{ mode: 'single' }"
        :show-borders="true"
        key-expr="id"
        @selection-changed="selectedChanged"
        @row-inserted="onRowInserted"
        @row-inserting="onRowInserting"
        @row-removing="onRowRemoving"
        @init-new-row="InitNewRow"
        @initialized="onInitialized"
        :height="342"
      >
        <DxToolbar  :visible="true">
          <DxItem name="groupPanel" />
          <DxItem template="addBtn-template"/> 
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
        </DxDataGridColumn>

        <DxDataGridColumn data-field="lastName" caption="Фамилия">
        </DxDataGridColumn>

        <DxDataGridColumn data-field="email">
        </DxDataGridColumn>

        <DxPager
          :visible="true"
          display-mode="full"
          :show-navigation-buttons="true"
        />
        <DxPaging :page-size="5"/>

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


        <DxEditing
            mode="popup"           
        >
          <DxPopup
            :show-title="true"
            :width="400"
            :min-height="300"
            :max-height="500"
            title="Добавить сотрудника"
          />
          <DxForm>
              
            <DxItemForm
              :col-count="1"
              :col-span="2"
              item-type="group"
            >
              <DxItemForm>
                <DxLookup
                  :data-source="dataSourceDxLookup"
                  :value="selectedId"
                  :items="dataSourceDxLookup"
                  :display-expr="customDisplayExpr"
                  @value-changed="onValueChanged"
                  value-expr="id"
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
  const emit = defineEmits<{
        (event: "return-dataSource", DataSorce: Array<Object>): void;
  }>();

  const props = defineProps({
    projectId: {
        type: Number
    },
    dataSourceg: {
      type:Array
    }
  })

  const dataSource = computed(() => props.dataSourceg)
  const selectedId = ref(-1)
  const dataSourceDxLookup = ref([])
  const selectedRowIndex = ref(-1)
  const dataGridRef = ref()
  const grid = computed(() => dataGridRef.value.instance);
  const projectId = ref(0)

  


  function customDisplayExpr(item:any){
    if(item != undefined){
      return `${item.firstName} ${item.lastName}`
    }
    return ""
  }



  function selectedChanged(e: any) {
    console.log(e);
    selectedRowIndex.value = e.component.getRowIndexByKey(e.selectedRowKeys[0]);
  }

  function addRow(e:any) {
    console.log(e)
    grid.value.addRow();
    console.log(`[addRow] projectId:${props.projectId}`)
    grid.value.deselectAll();
    console.log(`[addRow] projectId:${props.projectId}`)
  }

  function deleteRow(e:any) {
    grid.value.deleteRow(selectedRowIndex.value);
    grid.value.deselectAll();
  }

  async function onRowInserting(e:any) {
    console.log("onRowInserting");
    console.log(selectedId.value);
    await AddEmployee(selectedId.value)
    emit('return-dataSource', await GetProjectsWithEmployees())
  }

  async function onRowInserted(e:any) {
    console.log("OnRowInsterted");
    console.log(e.data);
    selectedId.value = -1
    await GetProjectsWithEmployees()
  }

  async function onRowRemoving(e:any) {
    console.log("onRowRemoving");
    console.log(e.data["id"]);
    const id = e.data["id"]
    await DeleteEmployee(id)
  }

  async function onValueChanged(e:any){
    console.log(e);
    selectedId.value = e.value
    await GetProjectsWithEmployees()
  }

  async function InitNewRow(e:any){
    console.log("InitNewRow")
    console.log(props.projectId)
    console.log(props.dataSourceg)
    console.log(dataGridRef.value.dataSource)
    await GetEmployees()
  }

  async function onInitialized(e:any) {
    console.log("onInitialized");
    console.log(e);
  }

  

  async function GetEmployees(){
        console.log("EmployeeDataGrid_GetEmployees")
        const res = await axios.get(
          `https://localhost:7104/api/Employee`
        )
        if(dataSource.value == undefined){
          dataSourceDxLookup.value = res.data
        }
        else{
          const data = await GetProjectsWithEmployees()
          const filterData = res.data.filter((newItem: {id:number}) => {
            return !data.some((existingItem:{id:number}) => existingItem.id === newItem?.id)
          })
          console.log(filterData)
          dataSourceDxLookup.value = filterData
        }
        
        console.log(dataSourceDxLookup.value)
  }

  async function GetProjectsWithEmployees(){
      console.log("GetProjectsWithEmployees")
      console.log(`[GetProjectsWithEmployees] projectId:${props.projectId}`)
      const res = await axios.get(
        `https://localhost:7104/api/Project/withEmployeesAndTasks/${props.projectId}`
      )
      return res.data.employees
  }
  async function GetEmployeesById(empId:Number){
      console.log("GetEmployeesById")
      console.log(`[GetEmployeesById] projectId:${empId}`)
      const res = await axios.get(
        `https://localhost:7104/api/Employee/${props.projectId}`
      )
      return res.data
  }

  async function AddEmployee(empId:Number){
        console.log("AddEmployee") 
        console.log(`[AddEmployee] projectId:${props.projectId}`)
        await axios.put(
          `https://localhost:7104/api/Project/${props.projectId}/addemployee`,
          {employeeId: empId}
        )
  }
  async function DeleteEmployee(empId:Number){
        console.log("DeleteEmployee") 
        console.log(`[DeleteEmployee] projectId:${props.projectId}`)
        await axios.delete(
          `https://localhost:7104/api/Project/${props.projectId}/deleteemployee/${empId}`
        )
  }
 

  </script>