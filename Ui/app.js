const routes=[
    {path:'/home',component:home},
    {path:'/usuario',component:usuario}
]

const router = new VueRouter.createRouter({
    history: VueRouter.createWebHashHistory(),
    routes,
})

const app = Vue.createApp({})

app.use(router)

app.mount('#app')