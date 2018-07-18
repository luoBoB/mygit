import request from '@/utils/request'

export function getListApi(data) {
  return request({
    url: '/Customer/GetList',
    method: 'post',
    data
  })
}

export function getNewApi(eiditId) {
  return request({
    url: '/Customer/GetNew?editId=' + eiditId,
    method: 'post'
  })
}

export function getRoleVersionOptionsApi() {
  return request({
    url: '/RoleVersion/GetList',
    method: 'post'
  })
}

