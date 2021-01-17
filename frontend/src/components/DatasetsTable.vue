<template>
  <b-table
    :items="modules"
    :fields="fields"
    bordered
    striped
    hover
    small
    show-empty
    empty-text="Нет данных для отображения"
  >
    <template v-slot:empty="{ emptyText }">
      <h4>{{ emptyText }}</h4>
    </template>
  </b-table>
</template>

<script>
import { toDDMMYYYY } from "../api/dateUtils";

export default {
  data() {
    return {
      fields: [
        {
          key: "name",
          label: "Имя",
          thClass: "vertical-align-top"
        },
        {
          key: "date",
          label: "Дата создания",
          thClass: "vertical-align-top",
          formatter: toDDMMYYYY
        },
        {
          key: "containsCyrillic",
          label: "Содержит кириллицу",
          thClass: "vertical-align-top",
          formatter: this.flagFormatter
        },
        {
          key: "containsLatin",
          label: "Содержит латиницу",
          thClass: "vertical-align-top",
          formatter: this.flagFormatter
        },
        {
          key: "containsNumbers",
          label: "Содержит цифры",
          thClass: "vertical-align-top",
          formatter: this.flagFormatter
        },
        {
          key: "containsSpecialCharacters",
          label: "Содержит специальные символы",
          thClass: "vertical-align-top",
          formatter: this.flagFormatter
        },
        {
          key: "caseSensitivity",
          label: "Чувствительность к регистру",
          thClass: "vertical-align-top",
          formatter: this.flagFormatter
        },
        {
          key: "locationOfResponsesToPictures",
          label: "Расположение ответов на картинки",
          thClass: "vertical-align-top"
        }
      ]
    };
  },
  created() {
    this.$store.dispatch("LOAD_MODULES");
  },
  computed: {
    modules() {
      return this.$store.getters.MODULES;
    }
  },
  methods: {
    flagFormatter(value) {
      return value ? "Да" : "Нет";
    }
  }
};
</script>

<style lang="css">
table.table {
  width: max-content;
}
.table thead th.vertical-align-top {
  vertical-align: top;
}
</style>
