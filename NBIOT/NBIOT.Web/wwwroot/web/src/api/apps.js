import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/Apps/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/Apps/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/Apps/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/Apps/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/Apps/Del',
    method: 'post',
    data
  })
}

export function getNewAppsApi() {
  return request({
    url: '/Apps/GetNew',
    method: 'post'
  })
}

export function getAppsListApi(data) {
  return request({
    url: '/Apps/GetList',
    method: 'post',
    data
  })
}

export function addAppsApi(data) {
  return request({
    url: '/Apps/Add',
    method: 'post',
    data
  })
}

export function delAppsApi(data) {
  return request({
    url: '/Apps/Del',
    method: 'post',
    data
  })
}
