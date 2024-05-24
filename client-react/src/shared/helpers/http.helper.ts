import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export function handleHttpError(error: any) {
  if (error.response) {
    toast.error(`Error ${error.response.status}: ${error.response.data}`);
  } else if (error.request) {
    toast.error("No response received from server.");
  } else {
    toast.error(`Error: ${error.message}`);
  }
  return Promise.reject(error);
}
