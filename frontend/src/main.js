import Vue from "vue";
import App from "./App.vue";
import { store } from "./store/index";
import router from "./router";
import BootstrapVue from "bootstrap-vue";
import UniqueId from "vue-unique-id";
import "./custom.scss";

Vue.use(BootstrapVue);
Vue.use(UniqueId);

Vue.config.productionTip = false;

export const eventBus = new Vue();

new Vue({
  store,
  router,
  render: h => h(App)
}).$mount("#app");
