import config from "../app/config";

export default class AuthService {
    private static baseUrl = config.apiUrl + '/api/auth/';

    static async challenge(provider: string) {
        window.location.href = `${this.baseUrl}challenge/${provider}`;
    }
}