import Vue from 'vue'
import Router from 'vue-router'
const _import = require('./_import_' + process.env.NODE_ENV)
// in development-env not use lazy-loading, because lazy-loading too many pages will cause webpack hot update too slow. so only in production use lazy-loading;
// detail: https://panjiachen.github.io/vue-element-admin-site/#/lazy-loading

Vue.use(Router)

/* Layout */
import Layout from '../views/layout/Layout'

/** note: submenu only apppear when children.length>=1
*   detail see  https://panjiachen.github.io/vue-element-admin-site/#/router-and-nav?id=sidebar
**/

/**
* hidden: true                   if `hidden:true` will not show in the sidebar(default is false)
* alwaysShow: true               if set true, will always show the root menu, whatever its child routes length
*                                if not set alwaysShow, only more than one route under the children
*                                it will becomes nested mode, otherwise not show the root menu
* redirect: noredirect           if `redirect:noredirect` will no redirct in the breadcrumb
* name:'router-name'             the name is used by <keep-alive> (must set!!!)
* meta : {
    roles: ['admin','editor']     will control the page roles (you can set multiple roles)
    title: 'title'               the name show in submenu and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar,
    noCache: true                if true ,the page will no be cached(default is false)
  }
**/
export const constantRouterMap = [
  { path: '/login', component: _import('login/index'), hidden: true },
  { path: '/authredirect', component: _import('login/authredirect'), hidden: true },
  { path: '/404', component: _import('errorPage/404'), hidden: true },
  { path: '/401', component: _import('errorPage/401'), hidden: true },
  {
    path: '',
    component: Layout,
    redirect: 'dashboard',
    children: [{
      path: 'dashboard',
      component: _import('dashboard/index'),
      name: 'dashboard',
      meta: { title: '首页', icon: 'dashboard', noCache: true }
    }]
  }
  // {
  //   path: '/documentation',
  //   component: Layout,
  //   redirect: '/documentation/index',
  //   children: [{
  //     path: 'index',
  //     component: _import('documentation/index'),
  //     name: 'documentation',
  //     meta: { title: '文档', icon: 'documentation', noCache: true }
  //   }]
  // }
]

export default new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({ y: 0 }),
  routes: constantRouterMap
})

export const asyncRouterMap = [
  {
    path: '/apps',
    component: Layout,
    redirect: '/apps/apps-list',
    name: 'apps',
    meta: {
      title: '应用管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'apps-list',
        component: _import('apps/appsList'),
        name: 'appsList',
        meta: {
          title: '应用列表',
          icon: 'documentation'
        }
      }
    ]
  },
  {
    path: '/admin',
    component: Layout,
    redirect: '/admin/admin-list',
    name: 'admin',
    meta: {
      title: '账号管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'admin-list',
        component: _import('admin/adminList'),
        name: 'adminList',
        meta: {
          title: '账号列表',
          icon: 'documentation'
        }
      }
    ]
  },
  {
    path: '/user',
    component: Layout,
    redirect: '/user/user-list',
    name: 'user',
    meta: {
      title: '用户管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'user-list',
        component: _import('user/userList'),
        name: 'userList',
        meta: {
          title: '用户列表',
          icon: 'documentation'
        }
      }
    ]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/profile-list',
    name: 'profile',
    meta: {
      title: 'profile管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'profile-list',
        component: _import('profile/profileList'),
        name: 'profileList',
        meta: {
          title: 'profile列表',
          icon: 'documentation'
        }
      },
      {
        path: 'protocol-list',
        component: _import('protocol/protocolList'),
        name: 'protocolList',
        meta: {
          title: '协议列表',
          icon: 'documentation'
        }
      }
    ]
  },
  {
    path: '/subscription',
    component: Layout,
    redirect: '/subscription/subscription-list',
    name: 'subscription',
    meta: {
      title: '订阅管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'subscription-list',
        component: _import('subscription/subscriptionList'),
        name: 'subscriptionList',
        meta: {
          title: '订阅列表',
          icon: 'documentation'
        }
      }
    ]
  },
  {
    path: '/userDevice',
    component: Layout,
    redirect: '/userDevice/user-device-list',
    name: 'userDevice',
    meta: {
      title: '设备管理',
      icon: 'documentation'
    },
    alwaysShow: true,
    children: [
      {
        path: 'user-device-list',
        component: _import('userDevice/userDeviceList'),
        name: 'userDeviceList',
        meta: {
          title: '设备列表',
          icon: 'documentation'
        }
      },
      {
        path: 'history-data-list',
        component: _import('history-data/historyDataList'),
        name: 'historyDataList',
        meta: {
          title: '历史数据',
          icon: 'documentation'
        }
      },
      {
        path: 'command-list',
        component: _import('command/commandList'),
        name: 'commandList',
        meta: {
          title: '历史命令',
          icon: 'documentation'
        }
      }
    ]
  }
  // {
  //   path: '/deviceData',
  //   component: Layout,
  //   redirect: '/deviceData/history-data-list',
  //   name: 'deviceData',
  //   meta: {
  //     title: '设备数据',
  //     icon: 'documentation'
  //   },
  //   alwaysShow: true,
  //   children: [
  //     {
  //       path: 'history-data-list',
  //       component: _import('history-data/historyDataList'),
  //       name: 'historyDataList',
  //       meta: {
  //         title: '历史数据',
  //         icon: 'documentation'
  //       }
  //     },
  //     {
  //       path: 'command-list',
  //       component: _import('command/commandList'),
  //       name: 'commandList',
  //       meta: {
  //         title: '历史命令',
  //         icon: 'documentation'
  //       }
  //     }
  //   ]
  // }
]
