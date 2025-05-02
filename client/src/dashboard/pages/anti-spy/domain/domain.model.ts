import { jwtDecode } from 'jwt-decode';
import { dashboard } from "@wix/dashboard";
import { ResponseResult } from '../models/response';

export class Guid {
  static NewGuid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function (c: any) {
        var r = (Math.random() * 16) | 0,
          v = c === "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      }
    );
  }
}
interface DecodedToken {
  data: string;
  [key: string]: any;
}

interface WixData {
  instance?: {
    instanceId?: string;
  };
}

export class wixProvider {
  static async getInstanceId(){
    try {
      const tokenWithPrefix = await dashboard.getAccessToken();
      const token = tokenWithPrefix.replace(/^OauthNG\.JWS\./, "");
      const decoded = jwtDecode<DecodedToken>(token);
      const instanceId = (JSON.parse(decoded.data) as WixData).instance?.instanceId ?? null;
      return instanceId;
    } catch (error) {
      console.error("Failed to get instanceId:", error);
      return null;
    }
  }

  static init(){
    (String.prototype as any).format = function () {
      var args = arguments;
      const stringReplace = this.replace(
        /{(\d+)}/g,
        function (match: any, number: any) {
          return typeof args[number] != "undefined" ? args[number] : match;
        }
      );
      return stringReplace;
    };
    (String.prototype as any).thenResultErrorIf = function (error_message: string) {
      const respError: ResponseResult<any> = {
        success: false,
        error: {
          error_code: "error",
          error_message: error_message,
          log_id: Guid.NewGuid(),
        },
      };
      return respError;
    };
  }
}
