import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/Protocol/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/Protocol/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/Protocol/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/Protocol/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/Protocol/Del',
    method: 'post',
    data
  })
}

export function getNewProtocolApi() {
  return request({
    url: '/Protocol/GetNew',
    method: 'post'
  })
}

export function getProtocolListApi(data) {
  return request({
    url: '/Protocol/GetList',
    method: 'post',
    data
  })
}

export function addProtocolApi(data) {
  return request({
    url: '/Protocol/Add',
    method: 'post',
    data
  })
}

export function delProtocolApi(data) {
  return request({
    url: '/Protocol/Del',
    method: 'post',
    data
  })
}
