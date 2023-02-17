import axios from "axios";
import config from "../../app/config";
import { GetIdentityResponse as GetCurrentIdentityResponse } from "./types";

export default class IdentityService {
  private static baseUrl = config.apiUrl + "/api/identity/";

  static async getCurrentIdentity(): Promise<GetCurrentIdentityResponse> {
    const response = await axios.get(this.baseUrl + "current");
    return response?.data;
  }
}
