import { AntiSetting } from "dashboard/pages/anti-spy/models/antispy-setting";
import React, { useState } from "react";
import { AntiSettingDto } from "./model";
import { Store } from "../models/store";
import { events } from "site/embedded-scripts/anti-spy-scripts/events";

type IStateManager = {
  setting: AntiSetting;
  settingPrev: AntiSetting;
  store?: Store;
  setState: (setting: AntiSetting, store?: Store) => void;
  setStatePrev: (settingPrev: AntiSetting) => void;

  eventSettings?: events;
  resetEventSettings: () => void;
};

export const stateContext = React.createContext<IStateManager>({
  setting: AntiSettingDto.Init(),
  settingPrev: AntiSettingDto.Init(),
  setState: () => {},
  setStatePrev: () => {},
  resetEventSettings: () => {},
});

export function StateContextProvider({ children }: any) {
  const [stateSettingPrev, setStateSettingPrev] = useState<AntiSetting>(
    AntiSettingDto.Init()
  );
  const [stateSetting, setStateSetting] = useState<AntiSetting>(
    AntiSettingDto.Init()
  );

  const [store, setStore] = useState<Store | undefined>();
  const [eventSettings, setEventSettings] = useState<events | undefined>();

  const setState = (setting: AntiSetting, store?: Store) => {
    if (!setting) return;
    setStateSetting({ ...setting });
    if (!store) return;
    setStore(store);
    const event = new events(
      setting,
      document.getElementById("anti-spy-preview"),
      false
    );
    setEventSettings(event);
    event.init();
  };

  const setStatePrev = (settingPrev: AntiSetting) => {
    setStateSettingPrev({ ...settingPrev });
  };

  const resetEventSettings = () => {
    if (!eventSettings) return;
    eventSettings.setting = stateSetting;
    setEventSettings(eventSettings);
  };

  return (
    <stateContext.Provider
      value={{
        resetEventSettings,
        eventSettings,
        store,
        setting: stateSetting,
        settingPrev: stateSettingPrev,
        setState: setState,
        setStatePrev: setStatePrev,
      }}
    >
      {children}
    </stateContext.Provider>
  );
}
