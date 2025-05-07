
public class SettingsRequest
{
    public bool ProtectImages { get; set; } 
    public bool ProtectText { get; set; }
    public bool StopKeyboardShortcuts { get; set; } 
    public bool ShowAlertMessage { get; set; }
    public string SelectionDisabledMessage { get; set; }

    // ==== Alert Configuration ====
    public int DisappearAfterSeconds { get; set; }

    // ==== Legal Notice Settings ====
    public bool LogLegalNotice { get; set; }
    public string LegalHeader { get; set; }
    public string LegalFooter { get; set; }
    public string LegalContent { get; set; }
    public SettingsEntity ToEntity()
    {
        return new SettingsEntity
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
