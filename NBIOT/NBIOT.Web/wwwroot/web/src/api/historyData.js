import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/HistoryData/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/HistoryData/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/HistoryData/Add',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/HistoryData/Del',
    method: 'post',
    data
  })
}
