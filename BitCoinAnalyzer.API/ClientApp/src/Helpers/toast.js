import { toast } from "react-toastify";

function success(msg) {
  toast.success(msg);
}

function error(msg) {
  toast.error(msg);
}

export { success, error };
