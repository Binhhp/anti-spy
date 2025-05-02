import { AntiSetting } from './antispy-setting';
export class SetSettingsRequest {
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
    constructor(dto: AntiSetting){
        this.protectImages = dto.protectImages;
        this.protectText = dto.protectText;
        this.stopKeyboardShortcuts = dto.stopKeyboardShortcuts;
        this.showAlertMessage = dto.showAlertMessage;
        this.selectionDisabledMessage = dto.selectionDisabledMessage;
        this.disappearAfterSeconds = dto.disappearAfterSeconds;
        this.logLegalNotice = dto.logLegalNotice;
        this.legalHeader = dto.legalHeader;
        this.legalFooter = dto.legalFooter;
        this.legalContent = dto.legalContent;
    }
}