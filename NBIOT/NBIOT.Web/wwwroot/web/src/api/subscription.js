import request from '@/utils/request'

export function getNewApi() {
  return request({
    url: '/Subscription/GetNew',
    method: 'post'
  })
}

export function getListApi(data) {
  return request({
    url: '/Subscription/GetList',
    method: 'post',
    data
  })
}

export function addApi(data) {
  return request({
    url: '/Subscription/AddSubscription',
    method: 'post',
    data
  })
}

export function updateApi(data) {
  return request({
    url: '/Subscription/Update',
    method: 'post',
    data
  })
}

export function delApi(data) {
  return request({
    url: '/Subscription/DelSubscription',
    method: 'post',
    data
  })
}

export function getNewSubscriptionApi() {
  return request({
    url: '/Subscription/GetNew',
    method: 'post'
  })
}

export function getSubscriptionListApi(data) {
  return request({
    url: '/Subscription/GetList',
    method: 'post',
    data
  })
}

export function addSubscriptionApi(data) {
  return request({
    url: '/Subscription/Add',
    method: 'post',
    data
  })
}

export function delSubscriptionApi(data) {
  return request({
    url: '/Subscription/Del',
    method: 'post',
    data
  })
}
