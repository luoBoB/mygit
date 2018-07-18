const dialogVisible = {
  state: {
    dialogAddBanner: false,
    addBannerTitle: '添加商户',
    addBannerEditId: ''
  },
  mutations: {
    setDialogAddBanner: (state, data) => {
      state.dialogAddBanner = data
    },
    setAddBannerTitle: (state, data) => {
      state.addBannerTitle = data
    },
    setAddBannerEditId: (state, data) => {
      state.addBannerEditId = data
    }
  },
  actions: {
    setDialogAddBanner({ commit }, data) {
      commit('setDialogAddBanner', data)
    },
    setAddBannerTitle({ commit }, data) {
      commit('setAddBannerTitle', data)
    },
    setAddBannerEditId({ commit }, data) {
      commit('setAddBannerEditId', data)
    }
  }
}

export default dialogVisible
