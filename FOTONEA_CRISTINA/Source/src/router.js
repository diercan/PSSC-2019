import Vue from 'vue'
import Router from 'vue-router'
import Login from './views/Login.vue'
import Error404 from './views/Error404.vue'
import Register from './views/Register.vue'
import SickLeaveRequest from './views/SickLeaveRequest.vue'
import EmployeeAccDetails from './views/EmployeeAccDetails.vue'
// import axios from "axios"

Vue.use(Router)
/* eslint-disable */
export const router = new Router({
    mode: 'hash',
    routes: [
        {
            path: '/Register',
            name: 'Register',
            component: Register
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
            path: '/EmployeeAccDetails',
            name: 'EmployeeAccDetails',
            component: EmployeeAccDetails
        },
        {
            path: '/*',
            name: '404',
            component: Error404
        }
    ]

})
router.beforeEach((to, from, next) => {

    const publicPages = ['/', '/Login', '/SickLeaveRequest', '/EmployeeAccDetails'];
    const logPage =['/Login'];
    if(localStorage.getItem('isLogged') == "false" &&  to.fullPath == '/EmployeeAccDetails' )
    {
        return next('/Login');
    }
    next();
   
}
)
