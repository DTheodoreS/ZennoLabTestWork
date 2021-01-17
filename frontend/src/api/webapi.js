import axios from "axios";

const client = axios.create({
  baseURL: "http://localhost:5000/api/dataset",
  json: true
});

export default {
  execute(method, resource, data) {
    return client({
      method,
      url: resource,
      data
    }).then(req => {
      return req.data;
    });
  },
  get(resource) {
    return this.execute("get", resource);
  },
  post(data, progressCallback) {
    return axios
      .post("/api/dataset", data, {
        onUploadProgress: event => {
          if (progressCallback != null) {
            progressCallback(event);
          }
        }
      })
      .then(response => {
        return response.data;
      });
  }
};
