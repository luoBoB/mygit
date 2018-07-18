import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/Admin/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/Admin/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/Admin/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/Admin/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/Admin/Del',
    method: 'post',
    data
  })
}

export function getNewAdminApi() {
  return request({
    url: '/Admin/GetNew',
    method: 'post'
  })
}

export function getAdminListApi(data) {
  return request({
    url: '/Admin/GetList',
    method: 'post',
    data
  })
}

export function addAdminApi(data) {
  return request({
    url: '/Admin/Add',
    method: 'post',
    data
  })
}

export function delAdminApi(data) {
  return request({
    url: '/Admin/Del',
    method: 'post',
    data
  })
}
