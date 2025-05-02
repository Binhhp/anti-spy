import { AntiSetting } from "dashboard/pages/anti-spy/models/antispy-setting";

export class events {
  $target?: Document;
  appendBodyAlert?: boolean;
  setting: AntiSetting;
  constructor(setting: AntiSetting, target?: any, appendBodyAlert?: boolean){
    this.$target = target ?? document;
    this.appendBodyAlert = appendBodyAlert ?? true;
    this.setting = setting;
  }
  initProtection = () => {
    if (this.setting?.protectImages) {
      this.protectImages.use();
    }
    if (this.setting?.protectText) {
        this.protectText.use();
    }
    if (this.setting?.stopKeyboardShortcuts) {
        this.stopKeyboardShortcuts.use();
    }
    if (this.setting?.logLegalNotice) {
        this.logLegalNotice(
          this.setting.logLegalNotice,
          this.setting.legalHeader,
          this.setting.legalContent,
          this.setting.legalFooter
      );
    }
  }
  protectImages = {
    use: () => {
      this.$target?.querySelectorAll("img").forEach((img) => {
        img.addEventListener("contextmenu", this.protectImages._onImgContextMenu);
        img.addEventListener("dragstart", this.protectImages._onImgDragStart);
      });
    },
    remove: () => {
      this.$target?.querySelectorAll("img").forEach((img) => {
        img.removeEventListener("contextmenu", this.protectImages._onImgContextMenu);
        img.removeEventListener("dragstart",this.protectImages._onImgDragStart);
      });
    },
    _onImgContextMenu: (e: Event) => {
      e.preventDefault();
      this.showPopupAlert();
    },
    _onImgDragStart: (e: Event) => {
      e.preventDefault();
      this.showPopupAlert();
    }
  }

  protectText = {
    use: () => {
      if(this.appendBodyAlert) document.body.style.userSelect = "none";
      else (this.$target as any).style.userSelect = "none";
      this.$target?.addEventListener("selectstart", this.protectText._onSelectStart);
      this.$target?.addEventListener("copy", this.protectText._onCopy);
    },
    remove: () => {
      if(this.appendBodyAlert) document.body.style.userSelect = "auto";
      else (this.$target as any).style.userSelect = "auto";
      this.$target?.removeEventListener("selectstart", this.protectText._onSelectStart);
      this.$target?.removeEventListener("copy", this.protectText._onCopy);
    },
    _onSelectStart: (e: Event) => {
      e.preventDefault();
      this.showPopupAlert();
    },
    _onCopy: (e: Event) => {
      e.preventDefault();
      this.showPopupAlert();
    }
  }

  stopKeyboardShortcuts = {
    use: () => {
      document.addEventListener("keydown", this.stopKeyboardShortcuts._onKeydown);
    },
    remove: () => {
      document.removeEventListener("keydown", () => {});
    },
    _onKeydown: (e: KeyboardEvent) => {
      const key = e.key.toLowerCase();
      if (
        (e.ctrlKey && ["a", "c", "v"].includes(key)) ||
        (e.shiftKey &&
          ["ArrowUp", "ArrowDown", "ArrowLeft", "ArrowRight"].includes(e.key))
      ) {
        e.preventDefault();
        this.showPopupAlert();
      }
    }
  }
  logLegalNotice(
    logLegalNotice: boolean,
    legalHeader: string,
    legalContent: string,
    legalFooter: string
  ) {
    if (logLegalNotice) {
      console.clear();
      console.log(
        `%c${legalHeader}\n\n%c${legalContent}\n\n%c${legalFooter}`,
        "color: red; font-weight: bold; font-size: 16px;",
        "color: black; font-weight: 600; font-size: 13px;",
        "color: blue; font-weight: bold;"
      );
    }
  }
  private showPopupAlert = () => {
    if (!this.setting.showAlertMessage || !this.setting.selectionDisabledMessage || !this.setting.disappearAfterSeconds) return;
    if (document.getElementById("anti-spy-alert")) return;
    const alertBox = document.createElement("div");
    alertBox.id = "anti-spy-alert";
    alertBox.innerHTML = `
            <span class="anti-spy-alert-warningicon">
                <svg width="29" height="29" viewBox="0 0 29 29" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g clip-path="url(#clip0_22045_89864)">
                        <path d="M14.5001 10.875V15.7083M14.5001 20.5417H14.5121M12.4338 4.66417L2.19922 21.75C1.98821 22.1154 1.87656 22.5297 1.87538 22.9517C1.87419 23.3737 1.98352 23.7886 2.19249 24.1552C2.40145 24.5218 2.70277 24.8273 3.06645 25.0413C3.43014 25.2553 3.84352 25.3704 4.26547 25.375H24.7346C25.1566 25.3704 25.57 25.2553 25.9337 25.0413C26.2973 24.8273 26.5987 24.5218 26.8076 24.1552C27.0166 23.7886 27.1259 23.3737 27.1247 22.9517C27.1236 22.5297 27.0119 22.1154 26.8009 21.75L16.5663 4.66417C16.3509 4.30905 16.0476 4.01544 15.6857 3.81167C15.3237 3.6079 14.9154 3.50085 14.5001 3.50085C14.0847 3.50085 13.6764 3.6079 13.3144 3.81167C12.9525 4.01544 12.6492 4.30905 12.4338 4.66417Z" stroke="#EC221F" stroke-width="4" stroke-linecap="round" stroke-linejoin="round"/>
                    </g>
                    <defs>
                    <clipPath id="clip0_22045_89864">
                    <rect width="29" height="29" fill="white"/>
                    </clipPath>
                    </defs>
                </svg>
            </span>
            <span class="anti-spy-alert-text">${this.setting.selectionDisabledMessage}</span>
        `;
    (this.appendBodyAlert ? this.$target?.body : this.$target)?.appendChild(alertBox);

    setTimeout(() => {
        alertBox.remove();
    }, this.setting.disappearAfterSeconds * 1000);
  }
};
