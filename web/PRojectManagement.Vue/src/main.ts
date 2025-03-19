import 'devextreme/dist/css/dx.light.css';
import './assets/main.css'





import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import config from 'devextreme/core/config';

config({
    licenseKey: '9Y2HR-B2H61-1T7ZN-YJZF9-CG1BQ'
})


const app = createApp(App)

app.use(router)
app.use(config)
app.mount('#app')
