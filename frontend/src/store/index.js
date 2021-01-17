import Vue from "vue";
import Vuex from "vuex";
import api from "@/api/backendApi";

Vue.use(Vuex);

export const store = new Vuex.Store({
  state: {
    modules: []
  },

  getters: {
    MODULES: state => {
      return state.modules;
    }
  },

  mutations: {
    SET_MODULES: (state, payload) => {
      state.modules = payload;
    },

    ADD_MODULE: (state, payload) => {
      state.modules.push(payload);
    }
  },

  actions: {
    LOAD_MODULES: async context =>
      api.getAll().then(modules => context.commit("SET_MODULES", modules)),

    ADD_MODULE: async (context, payload) => {
      context.commit("ADD_MODULE", payload);
    }
  }
});
