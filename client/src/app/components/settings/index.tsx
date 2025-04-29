import React from "react";
import { Box, Button, Page, WixDesignSystemProvider } from "@wix/design-system";
import "@wix/design-system/styles.global.css";
import { AppSettingsContainer } from "./app-settings/app-settings.styled";
import GeneralSettings from "./app-settings/general-settings";
import { StateContextProvider } from "./state-manager";
import AppPreview from "./app-preview";
import AdvanceSetting from "./app-settings/advance-settings";

export default function AntiSpy() {
  const AppSettings = () => {
    return (
      <WixDesignSystemProvider features={{ newColorsBranding: true }}>
        <AppSettingsContainer>
          <Page>
            <Page.Header
              title="AntiSpy"
              subtitle="Lock Down Your Content, Stop Copycats in Their Tracks!"
              actionsBar={
                <Box gap="12px" direction="horizontal">
                  <Button priority="secondary" skin="inverted">
                    Cancel
                  </Button>
                  <Button>Save</Button>
                </Box>
              }
            />
            <Page.Content>
              <Box
                width="100%"
                align="center"
                gap="20px"
                direction="horizontal"
              >
                <Box width="65%" direction="vertical" gap="30px">
                  <GeneralSettings />
                  <AdvanceSetting />
                </Box>
                <Box width="35%" direction="vertical">
                  <AppPreview />
                </Box>
              </Box>
            </Page.Content>
          </Page>
        </AppSettingsContainer>
      </WixDesignSystemProvider>
    );
  };
  return <StateContextProvider>{AppSettings()}</StateContextProvider>;
}
