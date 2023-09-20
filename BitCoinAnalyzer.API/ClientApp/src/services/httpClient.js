import axios from "axios";
import { authenticationService } from "./authenticationService";
import { success, error } from "../Helpers/toast";

class httpClient {
    constructor() {
        const stringUser = localStorage.getItem("currentUser");
        const userObject = JSON.parse(stringUser) || "{}";
        const token = userObject.token || "{}";

        const instancer = axios.create({
            baseURL: `${(process.env.REACT_APP_API_URL)}/api`,
            headers: { Authorization: `Bearer ${token}` },
        });
        this.axiosInstance = instancer;
    }

    async get(url) {
        try {
            const resp = await this.axiosInstance.get(url).then((res) => res);
            if (resp.data.message) {
                success(resp.data.message);
            }
            return resp;
        } catch (resp_1) {
            if (resp_1.response.data.message) error(resp_1.response.data.message);
            if (resp_1.response !== undefined || resp_1.response.status === 401) {
                authenticationService.logout();
                return {};
            } else {
                return Promise.reject(resp_1);
            }
        }
    }
    async post(url, formData) {
        try {
            const resp = await this.axiosInstance
                .post(url, formData)
                .then((res) => res);

            if (resp.data.message) {
                success(resp.data);
            }
            return resp;
        } catch (resp_1) {
            console.log(resp_1);
            if (resp_1.response.data.message) error(resp_1.response.data.message);
            if (resp_1.response !== undefined && resp_1.response.status === 401) {
                authenticationService.logout();
                return {};
            } else {
                return Promise.reject(resp_1);
            }
        }
    }

    async put(url, formData) {
        try {
            const resp = await this.axiosInstance
                .put(url, formData)
                .then((res) => res);
            if (resp.data.message) {
                success(resp.data.message);
            }
            return resp;
        } catch (resp_1) {
            if (resp_1.response.data.message) error(resp_1.response.data.message);
            if (resp_1.response !== undefined && resp_1.response.status === 401) {
                authenticationService.logout();
                return {};
            } else {
                return Promise.reject(resp_1);
            }
        }
    }

    async getById(url, id) {
        try {
            const resp = await this.axiosInstance
                .get(url, { params: { id: id } })
                .then((res) => res);
            if (resp.data.message) {
                success(resp.data.message);
            }
            return resp;
        } catch (resp_1) {
            if (resp_1.response.data.message) error(resp_1.response.data.message);
            if (resp_1.response !== undefined && resp_1.response.status === 401) {
                authenticationService.logout();
                return {};
            } else {
                return Promise.reject(resp_1);
            }
        }
    }

    async delete(url, id) {
        try {
            const resp = await this.axiosInstance
                .delete(url, { params: { id: id } })
                .then((res) => res);
            if (resp.data.message) {
                success(resp.data.message);
            }
            return resp;
        } catch (resp_1) {
            if (resp_1.response.data.message) error(resp_1.response.data.message);
            if (resp_1.response !== undefined && resp_1.response.status === 401) {
                authenticationService.logout();
                return {};
            } else {
                return Promise.reject(resp_1);
            }
        }
    }

    setTokenOnLogin(token) {
        this.axiosInstance.defaults.headers = { Authorization: `Bearer ${token}` };
    }
    clearTokenOnLogout() {
        localStorage.removeItem("BankConverterToken");
        this.axiosInstance.defaults.headers = {};
    }
}
const httpInstance = new httpClient();
export { httpInstance };
