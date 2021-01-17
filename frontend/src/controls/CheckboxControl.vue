<template>
  <b-form-checkbox v-model="value" name="check-button" :state="state" switch>
    {{ label }}
  </b-form-checkbox>
</template>

<script>
export default {
  props: ["modelValue", "label", "state"],
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
    }
  }
};
</script>
