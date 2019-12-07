import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import Error404 from './views/Error404.vue'
import Home from './views/Home.vue'
import SickLeaveRequest from './views/SickLeaveRequest.vue'
import Details from './views/Details.vue'
// import axios from "axios"

Vue.use(Router)
/* eslint-disable */
export const router = new Router({
    mode: 'hash',
    routes: [
        {
            path: '/',
            name: 'Home',
            component: Home
        },
        {
            path: '/Login',
            name: 'Login',
            component: Login
        },
        {
            path: '/SickLeaveRequest',
            name: 'SickLeaveRequest',
            component: SickLeaveRequest
        },
        {
            path: '/Details',
            name: 'Details',
            component: Details
        },
        {
            path: '/*',
            name: '404',
            component: Error404
        }
    ]

})
router.beforeEach((to, from, next) => {

    const publicPages = ['/', '/Login', '/SickLeaveRequest', '/Details'];
    const logPage =['/Login'];
    if(localStorage.getItem('isLogged') == "false" && (to.fullPath == '/SickLeaveRequest' && to.fullPath == '/Details' ) )
    {
        return next('/Login');
    }
    next();
   
}
)
