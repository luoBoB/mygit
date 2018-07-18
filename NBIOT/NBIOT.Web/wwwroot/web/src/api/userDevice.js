import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/UserDevice/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/UserDevice/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/UserDevice/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/UserDevice/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/UserDevice/Del',
    method: 'post',
    data
  })
}

export function getNewUserDeviceApi() {
  return request({
    url: '/UserDevice/GetNew',
    method: 'post'
  })
}

export function getUserDeviceListApi(data) {
  return request({
    url: '/UserDevice/GetList',
    method: 'post',
    data
  })
}

export function addUserDeviceApi(data) {
  return request({
    url: '/UserDevice/Add',
    method: 'post',
    data
  })
}

export function delUserDeviceApi(data) {
  return request({
    url: '/UserDevice/Del',
    method: 'post',
    data
  })
}
