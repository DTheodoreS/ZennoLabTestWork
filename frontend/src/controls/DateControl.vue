<template>
  <b-form-group :label="label" :label-for="$id('input')">
    <b-form-datepicker
      :id="$id('input')"
      v-model="value"
      locale="ru"
      :start-weekday="1"
      :date-format-options="{
        year: 'numeric',
        month: 'numeric',
        day: 'numeric'
      }"
      :placeholder="placeholder || ''"
      :value-as-date="true"
      :required="required"
    ></b-form-datepicker>
  </b-form-group>
</template>

<script>
export default {
  props: ["modelValue", "label", "required", "placeholder"],
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
