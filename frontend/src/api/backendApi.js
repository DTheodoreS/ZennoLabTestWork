import webapi from "./webapi";
import uploadProgressEvent from "../events/uploadProgressEvent";

export default {
  getAll() {
    return webapi.get("/").then(modules => {
      return modules.map(this.parseServerDatasetDto);
    });
  },
  saveDataset(dataset, file) {
    const formData = this.createFormData(dataset, file);
    return webapi.post(formData, event => uploadProgressEvent.fireEvent(event));
  },
  parseServerDatasetDto(dataset) {
    dataset.date = new Date(dataset.date);
    return dataset;
  },
  createFormData(data, file) {
    const formData = new FormData();

    if (data != null) {
      const keys = Object.keys(data);
      for (const index in keys) {
        const key = keys[index];
        formData.append(key, data[key]);
      }
    }

    if (file != null) {
      formData.append(file.name, file);
    }

    return formData;
  }
};
