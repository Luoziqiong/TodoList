// initial state
const state = {
  isLogin: false,
}

// getters
const getters = {}

// actions
const actions = {}

// mutations
const mutations = {
  changeLoginState(state, isLogin) {
    state.isLogin = isLogin;
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
