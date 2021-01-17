import { shallowMount } from "@vue/test-utils";
import { createLocalVue } from '@vue/test-utils';
import BootstrapVue from "bootstrap-vue";
import UniqueId from "vue-unique-id";
import TextControl from "@/controls/TextControl.vue";

describe('Plugin', () => {
  const Vue = createLocalVue();
  Vue.use(BootstrapVue);
  Vue.use(UniqueId);

  describe("TextControl.vue", () => {
    it("renders model when passed", () => {
      const modelValue = "Any value";
      const wrapper = shallowMount(TextControl, {
        propsData: { modelValue }, localVue: Vue
      });
      expect(wrapper.html()).toMatch(modelValue);
    });
  });
});
