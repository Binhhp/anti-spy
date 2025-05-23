import { AntiSetting } from "dashboard/pages/anti-spy/models/antispy-setting";

export class AntiSettingDto {
  public static Init(): AntiSetting {
    const antiSettingDto: AntiSetting = {
      disappearAfterSeconds: 2,
      protectImages: true,
      protectText: true,
      stopKeyboardShortcuts: true,
      showAlertMessage: true,
      selectionDisabledMessage: "Content is protected !!",
      logLegalNotice: true,
      legalHeader: "** LEGAL NOTICE **",
      legalFooter: "Please exit this area immediately.",
      legalContent:
        "All site content, including files, images, video, and written content is the property of iStore. Any attempts to mimic said content, or use it as your own without the direct consent of iStore may result in LEGAL ACTION against YOU.",
    };
    return antiSettingDto;
  }
}
