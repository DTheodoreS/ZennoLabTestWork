<template>
  <div>
    <b-modal ref="my-modal" hide-footer hide-header centered scrollable>
      <b-form @submit="onSubmit">
        <text-control
          label="Имя:"
          v-model="form.name"
          required
          maxlength="15"
        />

        <date-control label="Дата создания:" v-model="form.date" />

        <b-form-group label="Обязательные параметры:">
          <checkbox-control
            label="Содержит кириллицу:"
            v-model="form.containsCyrillic"
            :state="checkRequiredAttributes"
          />
          <checkbox-control
            label="Содержит латиницу:"
            v-model="form.containsLatin"
            :state="checkRequiredAttributes"
          />
          <checkbox-control
            label="Содержит цифры:"
            v-model="form.containsNumbers"
            :state="checkRequiredAttributes"
          />
        </b-form-group>

        <b-form-group label="Дополнительные параметры:">
          <checkbox-control
            label="Содержит специальные символы:"
            v-model="form.containsSpecialCharacters"
          />
          <checkbox-control
            label="Чувствительность к регистру:"
            v-model="form.caseSensitivity"
          />
        </b-form-group>

        <select-control
          label="Расположение ответов на картинки"
          v-model="form.locationOfResponsesToPictures"
          :options="locationsOfResponsesToPictures"
          required
        ></select-control>

        <file-control
          ref="package"
          label="Архив картинок"
          v-model="form.file"
          accept=".zip"
        />

        <div class="text-align-right">
          <b-button type="submit" variant="primary">Добавить</b-button>
          <b-button
            type="button"
            variant="outline-primary"
            class="margin-left-20"
            @click="hideModal"
            >Закрыть</b-button
          >
        </div>
      </b-form>

      <upload-progress-dialog ref="upload-progress" />
      <upload-error-dialog ref="upload-error" />
    </b-modal>
  </div>
</template>

<script>
import api from "@/api/backendApi";
import TextControl from "@/controls/TextControl";
import DateControl from "@/controls/DateControl";
import CheckboxControl from "@/controls/CheckboxControl";
import SelectControl from "@/controls/SelectControl";
import FileControl from "@/controls/FileControl";
import UploadProgressDialog from "@/components/UploadProgressDialog";
import UploadErrorDialog from "@/components/UploadErrorDialog";

export default {
  components: {
    TextControl,
    DateControl,
    CheckboxControl,
    SelectControl,
    FileControl,
    UploadProgressDialog,
    UploadErrorDialog
  },
  data() {
    return {
      form: {
        name: null,
        date: new Date(),
        containsCyrillic: false,
        containsLatin: false,
        containsNumbers: false,
        containsSpecialCharacters: false,
        caseSensitivity: false,
        locationOfResponsesToPictures: null,
        file: null
      },
      locationsOfResponsesToPictures: [
        { text: "", value: null },
        "отсутствует",
        "в именах файлов",
        "в отдельном файле"
      ]
    };
  },
  computed: {
    checkRequiredAttributes() {
      return (
        this.form.containsCyrillic ||
        this.form.containsLatin ||
        this.form.containsNumbers
      );
    }
  },
  methods: {
    getCurrentDialog() {
      return this.$refs["my-modal"];
    },
    showModal() {
      this.getCurrentDialog().show();
    },
    hideModal() {
      this.getCurrentDialog().hide();
      this.resetForm();
    },
    resetForm() {
      this.form.name = null;
      this.form.date = new Date();
      this.form.containsCyrillic = false;
      this.form.containsLatin = false;
      this.form.containsNumbers = false;
      this.form.containsSpecialCharacters = false;
      this.form.caseSensitivity = false;
      this.form.locationOfResponsesToPictures = null;
      if (this.$refs["package"] != null) {
        this.$refs["package"].reset();
      }
    },
    onSubmit(event) {
      event.preventDefault();
      if (!this.isValid()) {
        return;
      }

      var dataset = this.getDataset();
      const self = this;
      api
        .saveDataset(dataset, this.form.file)
        .then(function(errors) {
          self.getProgressDialog().hideModal();
          if (errors.length > 0) {
            self.getErrorDialog().showModal(errors);
          } else {
            self.$store.dispatch("ADD_MODULE", dataset);
            self.hideModal();
          }
        })
        .catch(function(responce) {
          console.error(responce);
          self.getProgressDialog().hideModal();
          const error = {
            isValid: false,
            message: "Ошибка."
          };
          self.getErrorDialog().showModal([error]);
        });
      this.getProgressDialog().showModal();
    },
    isValid() {
      return this.checkRequiredAttributes && this.form.file != null;
    },
    getDataset() {
      var dataset = {
        name: this.form.name,
        date: this.form.date.toJSON(),
        containsCyrillic: this.form.containsCyrillic,
        containsLatin: this.form.containsLatin,
        containsNumbers: this.form.containsNumbers,
        containsSpecialCharacters: this.form.containsSpecialCharacters,
        caseSensitivity: this.form.caseSensitivity,
        locationOfResponsesToPictures: this.form.locationOfResponsesToPictures
      };
      return dataset;
    },
    getProgressDialog() {
      return this.$refs["upload-progress"];
    },
    getErrorDialog() {
      return this.$refs["upload-error"];
    }
  }
};
</script>

<style lang="css" scoped>
.margin-left-20 {
  margin-left: 20px;
}
.text-align-right {
  text-align: right;
}
</style>
