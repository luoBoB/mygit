import request from '@/utils/request'

export function getBannerListApi(data) {
  return request({
    url: '/Banner/GetBannerList',
    method: 'post',
    data
  })
}

export function getNewBannerApi(eiditId) {
  return request({
    url: '/Banner/GetNewBanner',
    method: 'post'
  })
}

export function saveBannerApi(data) {
  return request({
    url: '/Banner/AddBanner',
    method: 'post',
    data
  })
}

export function delBannerApi(data) {
  return request({
    url: '/Banner/DelImage',
    method: 'post',
    data
  })
}
