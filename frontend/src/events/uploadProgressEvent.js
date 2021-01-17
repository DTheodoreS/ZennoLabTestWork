import { eventBus } from "@/main";

export default {
  getEventName() {
    return "UploadProgressEvent";
  },
  fireEvent(event) {
    const payload = {};
    const progress = (event.loaded / event.total) * 100;
    payload.progress = Math.round(progress);
    eventBus.$emit(this.getEventName(), payload);
  },
  addListener(callback) {
    eventBus.$on(this.getEventName(), callback);
  },
  removeListener(callback) {
    eventBus.$off(this.getEventName(), callback);
  }
};
