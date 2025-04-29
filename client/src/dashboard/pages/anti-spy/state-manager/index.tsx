import { antiSetting } from "dashboard/pages/anti-spy/models/antispy-setting";
import React, { useState } from "react";
import { AntiSettingDto } from "./model";

type IStateManager = {
  setting: antiSetting;
  setState: (setting: antiSetting) => void;
};

export const stateContext = React.createContext<IStateManager>({
  setting: AntiSettingDto.Init(),
  setState: () => {},
});

export function StateContextProvider({ children }: any) {
  const [stateSetting, setStateSetting] = useState<antiSetting>(
    AntiSettingDto.Init()
  );
  const setState = (setting: antiSetting) => {
    setStateSetting({ ...setting });
  };

  return (
    <stateContext.Provider
      value={{
        setting: stateSetting,
        setState: setState,
      }}
    >
      {children}
    </stateContext.Provider>
  );
}
