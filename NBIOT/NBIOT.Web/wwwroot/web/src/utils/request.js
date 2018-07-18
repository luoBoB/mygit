import axios from 'axios'
import {
  Message, MessageBox
} from 'element-ui'
import store from '@/store'
import {
  getToken
} from '@/utils/auth'

// create an axios instance
const service = axios.create({
  baseURL: process.env.BASE_API, // api的base_url
  timeout: 30000 // request timeout
})

// request interceptor
service.interceptors.request.use(config => {
  // Do something before request is sent]
  if (store.getters.token) {
    config.headers['X-Token'] = getToken() // 让每个请求携带token-- ['X-Token']为自定义key 请根据实际情况自行修改
  }
  return config
}, error => {
  // Do something with request error
  console.log(error) // for debug
  Promise.reject(error)
})

// respone interceptor
service.interceptors.response.use(
  response => {
    /**
   * 下面的注释为通过response自定义code来标示请求状态，当code返回如下情况为权限有问题，登出并返回到登录页
   * 如通过xmlhttprequest 状态码标识 逻辑可写在下面error中
   */
    const res = response.data
    if (res.status === '10000') {
      MessageBox.alert(`<div role="alert" class="el-alert el-alert--info">
                        <i class="el-alert__icon el-icon-info"></i>
                          <div class="el-alert__content">
                            <span class="el-alert__title">会话超时，请重新登录</span>
                          </div>
                        </div>
                        <div style='margin-top:10px' class="el-input el-input--medium">
                          <input id='pwd' type="password" autocomplete="off" class="el-input__inner" placeholder='密码'>
                        </div>`, '登录', {
          dangerouslyUseHTMLString: true,
          confirmButtonText: '登录',
          showCancelButton: true,
          cancelButtonText: '切换用户',
          showClose: false,
          beforeClose: (action, instance, done) => {
            if (action === 'confirm') {
              instance.confirmButtonLoading = true
              instance.confirmButtonText = '登录中...'
              var username = store.getters.roles.UserName
              var pwd = document.getElementById('pwd').value
              var postData = { UserName: username, Password: pwd }
              store.dispatch('LoginByUsername', postData).then(() => {
                Message.success('登录成功')
                instance.confirmButtonLoading = false
                instance.confirmButtonText = '登录'
                done()
              }).catch((error) => {
                instance.confirmButtonLoading = false
                instance.confirmButtonText = '登录'
                Message.error(error)
              })
            } else if (action === 'cancel') {
              store.dispatch('LogOut').then(() => {
                location.reload()// In order to re-instantiate the vue-router object to avoid bugs
              })
            }
          }
        }).then(action => {

      })
      Message.error('会话超时，请重新登录')
    }
    return response
  },
  error => {
    console.log('err' + error) // for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  })

export default service
