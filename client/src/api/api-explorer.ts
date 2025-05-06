import {
  ApiResponse,
  ResponseResult,
} from "dashboard/pages/anti-spy/models/response";
import { SetSettingsRequest } from "dashboard/pages/anti-spy/models/set-setting";
import { EmbedScriptDto, Store } from "dashboard/pages/anti-spy/models/store";

const RootURL = import.meta.env.VITE_API_URL;
export class apiExplorer {
  static getBySiteIdAsync = async (
    siteId: string
  ): Promise<ResponseResult<Store>> => {
    const respJson = await fetch(`${RootURL}/sites/${siteId}`)
      .then((r) => r.json())
      .catch(() => siteId.thenResultErrorIf("Invalid site Id"));
    return respJson;
  };

  static getAsync = async (
    instanceId: string
  ): Promise<ResponseResult<Store>> => {
    const respJson = await fetch(`${RootURL}/stores/${instanceId}`)
      .then((r) => r.json())
      .catch(() => instanceId.thenResultErrorIf("Invalid instance Id"));
    return respJson;
  };

  static setAsync = async (
    instanceId: string,
    req: SetSettingsRequest
  ): Promise<ResponseResult<any>> => {
    if (!instanceId) return instanceId.thenResultErrorIf("Invalid instance Id");
    const resp = await fetch(`${RootURL}/stores/${instanceId}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: req ? JSON.stringify(req) : null,
    })
      .then((x) => x.json())
      .catch(() => instanceId.thenResultErrorIf("Invalid instance Id"));
    return resp;
  };

  static unInstallStore = async (instanceId?: string) => {
    if (!instanceId) return "".thenResultErrorIf("Invalid instance Id");
    await fetch(`${RootURL}/stores/${instanceId}/uninstall`, {
      method: "DELETE"
    });
    return ApiResponse.Success();
  };
  
  static embeddedScripts = async (instanceId?: string) : Promise<ResponseResult<EmbedScriptDto>> => {
    if (!instanceId) return "".thenResultErrorIf("Invalid instance Id");
    const resp = await fetch(`${RootURL}/stores/${instanceId}/embedded-scripts`).then(x => x.json())
      .catch(() => instanceId.thenResultErrorIf("Invalid instance Id"));
    return resp;
  };
}
