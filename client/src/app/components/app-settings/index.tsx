import React from "react";
import { AppSettingsContainer } from "./app-settings.styled";
import {
  Button,
  EmptyState,
  Page,
  WixDesignSystemProvider,
} from "@wix/design-system";
import "@wix/design-system/styles.global.css";
import GeneralSettings from "./general-settings";

export default function AppSettings() {
  return (
    <WixDesignSystemProvider features={{ newColorsBranding: true }}>
      <AppSettingsContainer>
        <Page>
          <Page.Header
            title="AntiSpy"
            subtitle="Lock Down Your Content, Stop Copycats in Their Tracks!"
            actionsBar={
              <div className="anti-spy-actions">
                <Button priority="secondary" skin="inverted">
                  Cancel
                </Button>
                <Button>Save</Button>
              </div>
            }
          />
          <Page.Content>
            <div className="anti-spy-settings">
              <GeneralSettings />
            </div>
            <div className="anti-spy-settings">
              <GeneralSettings />
            </div>
          </Page.Content>
        </Page>
      </AppSettingsContainer>
    </WixDesignSystemProvider>
  );
}
