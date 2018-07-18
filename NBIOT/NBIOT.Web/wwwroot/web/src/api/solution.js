import request from '@/utils/request'

export function getNewCategoryApi() {
  return request({
    url: '/SolutionCategory/GetNew',
    method: 'post'
  })
}

export function getCategoryListApi(data) {
  return request({
    url: '/SolutionCategory/GetList',
    method: 'post',
    data
  })
}

export function getCategoryAllApi(data) {
  return request({
    url: '/SolutionCategory/GetAll',
    method: 'post',
    data
  })
}

export function addCategoryApi(data) {
  return request({
    url: '/SolutionCategory/Add',
    method: 'post',
    data
  })
}

export function updateCategoryApi(data) {
  return request({
    url: '/SolutionCategory/Update',
    method: 'post',
    data
  })
}

export function delCategoryApi(data) {
  return request({
    url: '/SolutionCategory/Del',
    method: 'post',
    data
  })
}

export function getNewSolutionApi() {
  return request({
    url: '/Solution/GetNew',
    method: 'post'
  })
}

export function getSolutionListApi(data) {
  return request({
    url: '/Solution/GetList',
    method: 'post',
    data
  })
}

export function addSolutionApi(data) {
  return request({
    url: '/Solution/Add',
    method: 'post',
    data
  })
}

export function delSolutionApi(data) {
  return request({
    url: '/Solution/Del',
    method: 'post',
    data
  })
}
