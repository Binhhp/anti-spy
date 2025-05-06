import React, { FC, useContext, useEffect, useState } from "react";
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
import { StateContextProvider, stateContext } from "./state-manager";
import AppPreview from "./components/app-preview";
import AdvanceSetting from "./components/app-settings/advance-settings";
import MainLayout from "dashboard/pages/anti-spy/layout/main";
import { wixProvider } from "./domain/domain.model";
import { dashboard } from "@wix/dashboard";
import { SetSettingsRequest } from "./models/set-setting";
import { apiExplorer } from "api/api-explorer";
import { AntiSettingDto } from "./state-manager/model";

const AntiSpy: FC = () => {
  wixProvider.init();
  return (
    <StateContextProvider>
      <MainLayout>
        <AppSettings />
      </MainLayout>
    </StateContextProvider>
  );
};
export default AntiSpy;

const AppSettings: FC = () => {
  const [loading, setLoading] = useState(false);
  const state = useContext(stateContext);

  const getInstanceSetting = async () => {
    const instanceId = await wixProvider.getInstanceId();
    if (!instanceId) return;
    window.instanceId = instanceId;
    const resp = await apiExplorer.getAsync(instanceId);
    if (!resp?.data) {
      console.log("AntiSpy: Could not connect to api!!");
      return;
    }
    let settings = resp.data.settings;
    state.setState(settings, resp.data);
    state.setStatePrev(settings);
    if (!settings) {
      settings = AntiSettingDto.Init();
      if (resp.data.instanceId) {
        console.log("AntiSpy: create setting and embedded scripts");
        Promise.all([
          apiExplorer.setAsync(
            resp.data.instanceId,
            new SetSettingsRequest(settings)
          ),
          apiExplorer.embeddedScripts(resp.data.instanceId),
        ]);
      }
    }
  };

  useEffect(() => {
    getInstanceSetting();
  }, []);

  const saveChange = async () => {
    setLoading(true);
    const resp = await apiExplorer.setAsync(
      window.instanceId,
      new SetSettingsRequest(state.setting)
    );
    dashboard.showToast({
      message: resp.success
        ? "Settings saved successfully."
        : "Save settings fail.",
      type: resp.success ? "success" : "error",
      timeout: "normal",
    });
    setLoading(false);
    state.setStatePrev({ ...state.setting });
  };

  const onCancel = () => {
    if (!state.settingPrev) return;
    state.setState({ ...state.settingPrev });
  };

  return (
    <WixDesignSystemProvider features={{ newColorsBranding: true }}>
      <AppSettingsContainer>
        <Page>
          <Page.Header
            title="AntiSpy"
            subtitle="Lock Down Your Content, Stop Copycats in Their Tracks!"
            actionsBar={
              <Box gap="12px" direction="horizontal">
                <Button priority="secondary" skin="inverted" onClick={onCancel}>
                  Cancel
                </Button>
                <Button disabled={loading} onClick={saveChange}>
                  Save
                </Button>
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
