import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/Profile/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/Profile/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/Profile/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/Profile/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/Profile/Del',
    method: 'post',
    data
  })
}

export function getNewProfileApi() {
  return request({
    url: '/Profile/GetNew',
    method: 'post'
  })
}

export function getProfileListApi(data) {
  return request({
    url: '/Profile/GetList',
    method: 'post',
    data
  })
}

export function addProfileApi(data) {
  return request({
    url: '/Profile/Add',
    method: 'post',
    data
  })
}

export function delProfileApi(data) {
  return request({
    url: '/Profile/Del',
    method: 'post',
    data
  })
}
