import axios from "axios";
import config from "../app/config";

export default class IdentityService {
    private static baseUrl = config.apiUrl + '/api/identity/';

    static async getCurrentIdentity() {
        const response = await axios.get(this.baseUrl + 'current', { withCredentials: true });
        return response.data;
    }
}