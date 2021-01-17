<template>
  <b-form-group :label="label" :label-for="$id('select')">
    <b-form-select
      :id="$id('select')"
      v-model="value"
      :options="options"
      :required="required"
      :state="state"
    ></b-form-select>
  </b-form-group>
</template>

<script>
export default {
  props: ["modelValue", "label", "required", "options"],
  model: {
    prop: "modelValue",
    event: "update:modelValue"
  },
  emits: ["update:modelValue"],
  computed: {
    value: {
      get() {
        return this.modelValue;
      },
      set(value) {
        this.$emit("update:modelValue", value);
      }
    },
    state() {
      if (this.required == null || this.required != "") {
        return null;
      }

      return this.value != null;
    }
  }
};
</script>
