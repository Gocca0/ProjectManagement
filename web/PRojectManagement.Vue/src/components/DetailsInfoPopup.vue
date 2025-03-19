<template>
    <div id="popup">
        <DefaultPopup
        :visible="isPopupVisible"
        :show-title="true"
        title="Дополнительная информация"
        :max-width="1000"
        :height="800"
        @hidden="hiddenPopup"
        @showing="Showing"
        >
        <EmployeeDataGrid :project-id="projectId" :data-sourceg="gridEmployeeDataSource" @return-data-source="retDataSource"/>
        <TaskDataGrid :data-sourceg="gridTasksDataSource"/>
        <div>
            <DxButton
                text="Вывести в консоль пропсы"
                @click="Information"
            >
            </DxButton>
        </div>
        </DefaultPopup>
   </div>
   
</template>

<script setup lang="ts">
    import { DxPopup as DefaultPopup } from 'devextreme-vue/popup';
    import {computed, defineComponent, onMounted, onUpdated, reactive, ref, toRef} from 'vue';
    import { DxButton } from 'devextreme-vue/button';
    import EmployeeDataGrid from './EmployeeDataGrid.vue';
    import axios from 'axios';
import TaskDataGrid from './TaskDataGrid.vue';

    const emit = defineEmits<{
        (event: "hidden-popup", isActive: boolean): void;
    }>();


    const props = defineProps({
        isPopupVisible: {
            type: Boolean
        },
        dataSelectedRow: {
            type: Object
        },
        
    })
    const projectId = computed(() => props.dataSelectedRow?.id )
    const gridEmployeeDataSource =  ref()
    const gridTasksDataSource =  ref()
   
    
    function retDataSource(){
        console.log("retDataSource");
        GetProjectsWithEmployees(projectId.value)
    }

    function hiddenPopup(e:any){
        emit("hidden-popup", false)
    }
    function Showing(){
        console.log("Showing")
        GetProjectsWithEmployees(projectId.value)
    }
    function Information(){
        console.log(props.isPopupVisible)
        console.log(props.dataSelectedRow)
        console.log(projectId)
        console.log(gridEmployeeDataSource)
    }

    async function GetProjectsWithEmployees(id:number){
        console.log("GetProjectsWithEmployees")
        console.log(`[GetProjectsWithEmployees] projectId:${id}`)
        if (id != undefined){
            const res = await axios.get(
            `https://localhost:7104/api/Project/withEmployeesAndTasks/${id}`
            )
            console.log(res.data)
            gridEmployeeDataSource.value = res.data.employees
            gridTasksDataSource.value = res.data.tasks
            console.log(id)
            console.log(gridEmployeeDataSource.value)
            console.log("gridTasksDataSource.value")
            console.log(gridTasksDataSource.value)
        }
        else{
            console.log(`id=${id}`)
        }
        
  }

</script>