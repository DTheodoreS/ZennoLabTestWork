<template>
  <b-form-group :label="label" :label-for="$id('input')">
    <b-form-input
      :id="$id('input')"
      v-model="value"
      :required="required"
      :formatter="nameFormatter"
      :placeholder="placeholder || ''"
      :state="state"
    ></b-form-input>
  </b-form-group>
</template>

<script>
export default {
  props: ["modelValue", "label", "required", "placeholder", "maxlength"],
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

      return this.value != null && this.value.length > 0;
    }
  },
  methods: {
    nameFormatter(value) {
      if (this.maxlength == null || this.maxlength < 1) {
        return;
      }

      if (value == null || value.length <= this.maxlength) {
        return value;
      } else {
        return value.substr(0, this.maxlength);
      }
    }
  }
};
</script>
