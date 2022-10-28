import axios from "axios";
import config from "../../app/config";

export default class AuthService {
  private static baseUrl = config.apiUrl + "/api/auth/";

  static async challenge(provider: string) {
    window.location.href = `${this.baseUrl}challenge/${provider}`;
  }

  static async getProviders(): Promise<string[]> {
    const response = await axios.get(this.baseUrl + "providers");
    return response.data;
  }
}
