<template>
  <div class="container">
    <b-modal
      ref="my-modal"
      hide-footer
      hide-header
      no-close-on-backdrop
      centered
      scrollable
    >
      <h5>Найдены следующие ошибки:</h5>
      <ul>
        <li v-for="(error, index) in errors" :key="index">
          {{ error.message }}
        </li>
      </ul>

      <div class="text-align-right">
        <b-button type="button" variant="outline-primary" @click="hideModal">
          ОК
        </b-button>
      </div>
    </b-modal>
  </div>
</template>

<script lang="ts">
export default {
  data() {
    return {
      errors: []
    };
  },
  methods: {
    showModal(errors) {
      this.errors = errors;
      this.$refs["my-modal"].show();
    },
    hideModal() {
      this.$refs["my-modal"].hide();
    }
  }
};
</script>

<style lang="css" scoped>
.text-align-right {
  text-align: right;
}
</style>
