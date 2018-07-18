import request from '@/utils/request'

export function loginByUsername(UserName, Password) {
  const data = {
    UserName,
    Password
  }
  return request({
    url: '/Admin/Login',
    method: 'post',
    data
  })
}

export function logout(token) {
  return request({
    url: '/Admin/Logout?token=' + token,
    method: 'post'
  })
}

export function getUserInfo(token) {
  return request({
    url: '/Admin/GetUserInfo?token=' + token,
    method: 'post'
  })
}
