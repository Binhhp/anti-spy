import { AntiSetting } from "./antispy-setting";

export interface Store extends Base {
    instanceId: string;
    email: string | null;
    phone: string | null;
    country: string | null;
    siteId: string;
    siteUrl: string;
    siteDisplayName: string;
    siteDescription: string;
    appInstanceName: string;
    businessName: string | null;
    timezone: string | null;
    currency: string | null;
    settings: AntiSetting;
}

interface Base {
    id: string;
    createdTime: string;
    modifyTime: string;
}