import moment from "moment";

const toDDMMYYYY = function(date) {
  if (date == null) {
    return "";
  }

  return moment(date).format("DD.MM.YYYY");
};

export { toDDMMYYYY };
