<template>
  <b-form-group :label="label" :label-for="$id('file')">
    <b-form-file
      :id="$id('file')"
      v-model="value"
      ref="input-file"
      :state="Boolean(value)"
      :accept="accept"
      placeholder="Выберите файл или перетащите его сюда..."
      drop-placeholder="Перетащите файл сюда..."
    ></b-form-file>
  </b-form-group>
</template>

<script>
export default {
  props: ["modelValue", "label", "accept"],
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
  },
  methods: {
    reset() {
      this.$refs["input-file"].reset();
    }
  }
};
</script>
