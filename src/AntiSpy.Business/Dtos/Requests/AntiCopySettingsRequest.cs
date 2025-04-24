
public class AntiCopySettingsRequest
{
    public bool ProtectImages { get; set; } = true;
    public bool ProtectText { get; set; } = true;
    public bool StopKeyboardShortcuts { get; set; } = true;
    public bool ShowAlertMessage { get; set; } = true;
    public string SelectionDisabledMessage { get; set; } = "Content is protected !!";

    // ==== Alert Configuration ====
    public int DisappearAfterSeconds { get; set; } = 2;

    // ==== Legal Notice Settings ====
    public bool LogLegalNotice { get; set; } = true;
    public string LegalHeader { get; set; } = "** LEGAL NOTICE **";
    public string LegalFooter { get; set; } = "Please exit this area immediately.";
    public string LegalContent { get; set; } = "All site content, including files, images, video, and written content is the property of iStore. " +
        "Any attempts to mimic said content, or use it as your own without the direct consent of iStore may result in LEGAL ACTION against YOU.";
    public AntiCopySettingsEntity ToEntity()
    {
        return new AntiCopySettingsEntity
        {
            ProtectImages = this.ProtectImages,
            ProtectText = this.ProtectText,
            StopKeyboardShortcuts = this.StopKeyboardShortcuts,
            ShowAlertMessage = this.ShowAlertMessage,
            SelectionDisabledMessage = this.SelectionDisabledMessage,
            DisappearAfterSeconds = this.DisappearAfterSeconds,
            LogLegalNotice = this.LogLegalNotice,
            LegalHeader = this.LegalHeader,
            LegalContent = this.LegalContent,
            LegalFooter = this.LegalFooter
        };
    }
}
