import { ResponseResult } from "../dashboard/pages/anti-spy/models/response";
import { SetSettingsRequest } from "../dashboard/pages/anti-spy/models/set-setting";
import { Store } from "../dashboard/pages/anti-spy/models/store";
import { RootURL } from "./config";

export class apiExplorer {
    static getBySiteIdAsync = async (siteId: string): Promise<ResponseResult<Store>> => {
        try{
            const respJson = await fetch(`${RootURL}/sites/${siteId}`).then(r => r.json());
            return respJson;
        }
        catch{
            return siteId.thenResultErrorIf("Invalid site Id");
        }
    }

    static getAsync = async (instanceId: string): Promise<ResponseResult<Store>> => {
        try{
            const respJson = await fetch(`${RootURL}/stores/${instanceId}`).then(r => r.json());
            return respJson;
        }
        catch{
            return instanceId.thenResultErrorIf("Invalid instance Id");
        }
    }

    static setAsync = async (instanceId: string, req: SetSettingsRequest): Promise<ResponseResult<any>> => {
        try{
            if(!instanceId) return instanceId.thenResultErrorIf("Invalid instance Id");
            const resp = await fetch(`${RootURL}/stores/${instanceId}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: req ? JSON.stringify(req) : null
            }).then(x => x.json());
            return resp;
        }
        catch{
            return instanceId.thenResultErrorIf("Invalid instance Id");
        }
    }
}