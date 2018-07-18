import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/CommandInfo/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/CommandInfo/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/CommandInfo/Add',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/CommandInfo/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/CommandInfo/Del',
    method: 'post',
    data
  })
}

export function getNewCommandInfoApi() {
  return request({
    url: '/CommandInfo/GetNew',
    method: 'post'
  })
}

export function getCommandInfoListApi(data) {
  return request({
    url: '/CommandInfo/GetList',
    method: 'post',
    data
  })
}

export function addCommandInfoApi(data) {
  return request({
    url: '/CommandInfo/Add',
    method: 'post',
    data
  })
}

export function delCommandInfoApi(data) {
  return request({
    url: '/CommandInfo/Del',
    method: 'post',
    data
  })
}
