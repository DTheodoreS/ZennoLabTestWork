<template>
  <div class="container">
    <b-modal
      ref="my-modal"
      hide-footer
      hide-header
      no-close-on-backdrop
      centered
    >
      <b-form>
        <b-form-group
          id="fieldset-horizontal"
          label-cols-sm="4"
          label-cols-lg="4"
          content-cols-sm
          content-cols-lg="8"
          label="Отправка данных:"
        >
          <b-progress
            :value="progress"
            :max="100"
            show-progress
            animated
            height="2rem"
          ></b-progress>
        </b-form-group>
        <b-form-group
          id="fieldset-horizontal"
          label-cols-sm="4"
          label-cols-lg="4"
          content-cols-sm
          content-cols-lg="8"
          label="Проверка данных:"
        >
          <b-spinner
            variant="primary"
            label="Text Centered"
            v-if="uploadComplete"
          ></b-spinner>
        </b-form-group>
      </b-form>
    </b-modal>
  </div>
</template>

<script lang="ts">
import uploadProgressEvent from "../events/uploadProgressEvent";

export default {
  data() {
    return {
      progress: 0,
      uploadComplete: false
    };
  },
  created() {
    uploadProgressEvent.addListener(this.uploadProgressCallback);
  },
  destroyed() {
    uploadProgressEvent.removeListener(this.uploadProgressCallback);
  },
  methods: {
    uploadProgressCallback(details) {
      this.progress = details.progress;
      if (details.progress == 100) {
        this.uploadComplete = true;
      }
    },
    showModal() {
      this.resetForm();
      this.$refs["my-modal"].show();
    },
    hideModal() {
      this.$refs["my-modal"].hide();
    },
    resetForm() {
      this.progress = 0;
      this.uploadComplete = false;
    }
  }
};
</script>
