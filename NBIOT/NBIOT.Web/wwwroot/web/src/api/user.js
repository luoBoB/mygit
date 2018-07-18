import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/User/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/User/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/User/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/User/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/User/Del',
    method: 'post',
    data
  })
}

export function getNewUserApi() {
  return request({
    url: '/User/GetNew',
    method: 'post'
  })
}

export function getUserListApi(data) {
  return request({
    url: '/User/GetList',
    method: 'post',
    data
  })
}

export function addUserApi(data) {
  return request({
    url: '/User/Add',
    method: 'post',
    data
  })
}

export function delUserApi(data) {
  return request({
    url: '/User/Del',
    method: 'post',
    data
  })
}
