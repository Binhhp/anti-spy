export interface Base {
    id?: string;
    createdTime?: string;
    modifyTime?: string;
}
export interface AntiSetting extends Base {
    protectImages: boolean;
    protectText: boolean;
    stopKeyboardShortcuts: boolean;
    showAlertMessage: boolean;
    selectionDisabledMessage: string;
    disappearAfterSeconds: number;
    logLegalNotice: boolean;
    legalHeader: string;
    legalFooter: string;
    legalContent: string;
    storeId?: string;
    store?: Store;
  }

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
    token: string;
    refreshToken: string;
    currency: string | null;
}