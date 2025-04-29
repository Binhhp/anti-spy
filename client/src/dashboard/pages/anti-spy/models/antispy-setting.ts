export interface base {
    id?: string;
    createdTime?: string;
    modifyTime?: string;
}
export interface antiSetting extends base {
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
    store?: store;
  }

export interface store extends base {
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