<template>
  <div class="home">
    <h1 v-if="false">ZennoLab Тестовое задание</h1>
    <modules-table />
    <br />
    <b-button variant="primary" @click="openModal">Добавить модуль</b-button>
    <upload-form-dialog ref="upload-modal-form" />
  </div>
</template>

<script>
import ModulesTable from "@/components/DatasetsTable";
import UploadFormDialog from "@/components/UploadFormDialog";

export default {
  name: "Home",
  components: {
    ModulesTable,
    UploadFormDialog
  },
  methods: {
    openModal() {
      this.$refs["upload-modal-form"].showModal();
    }
  }
};
</script>

<style lang="css">
.home {
  text-align: -webkit-center;
}
</style>
