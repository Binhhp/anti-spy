import React, { FC } from "react";
import {
  Box,
  Button,
  Cell,
  Layout,
  Page,
  WixDesignSystemProvider,
} from "@wix/design-system";
import "@wix/design-system/styles.global.css";
import { AppSettingsContainer } from "./components/app-settings/app-settings.styled";
import GeneralSettings from "./components/app-settings/general-settings";
import { StateContextProvider } from "./state-manager";
import AppPreview from "./components/app-preview";
import AdvanceSetting from "./components/app-settings/advance-settings";
import MainLayout from "dashboard/pages/anti-spy/layout/main";

const AntiSpy: FC = () => {
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
              <Layout>
                <Cell>
                  <Layout gap={27}>
                    <Cell span={8}>
                      <Box direction="vertical" gap="30px">
                        <GeneralSettings />
                        <AdvanceSetting />
                      </Box>
                    </Cell>
                    <Cell span={4}>
                      <Box direction="vertical">
                        <AppPreview />
                      </Box>
                    </Cell>
                  </Layout>
                </Cell>
              </Layout>
            </Page.Content>
          </Page>
        </AppSettingsContainer>
      </WixDesignSystemProvider>
    );
  };
  return (
    <StateContextProvider>
      <MainLayout>{AppSettings()}</MainLayout>
    </StateContextProvider>
  );
};
export default AntiSpy;
