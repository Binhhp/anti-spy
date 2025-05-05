import {
  Box,
  Card,
  Checkbox,
  FormField,
  Input,
  InputArea,
  NumberInput,
  Text,
} from "@wix/design-system";
import React, { FC } from "react";
import { stateContext } from "../../state-manager";

const AdvanceSetting: FC = () => {
  const state = React.useContext(stateContext);
  const onChange = (key: "showAlertMessage" | "logLegalNotice") => () => {
    state.setting[key] = !state.setting[key];
    state.setState(state.setting);

    state.resetEventSettings();
  };

  const onChangeInput =
    (
      key:
        | "selectionDisabledMessage"
        | "disappearAfterSeconds"
        | "legalHeader"
        | "legalFooter"
        | "legalContent"
    ) =>
    (e: any) => {
      if (key === "disappearAfterSeconds") {
        state.setting.disappearAfterSeconds = e;
      } else {
        state.setting[key] = e.target.value;
      }
      state.setState(state.setting);
    };

  const getCheckboxLabel = (item: { label: string; desc: string }) => (
    <Box direction="vertical">
      <Text>{item.label}</Text>
      <Text size="small" secondary>
        {item.desc}
      </Text>
    </Box>
  );

  const onResetEvent = () => {
    state.resetEventSettings();
  };

  const onBlurDissapear = (e: React.FocusEvent<HTMLInputElement, Element>) => {
    if (
      !e.target.value ||
      Number(e.target.value) < 0 ||
      Number(e.target.value) > 11
    ) {
      state.setting.disappearAfterSeconds = 2;
      state.setState(state.setting);
    }
    onResetEvent();
  };

  return (
    <Card className="anti-spy-advance-settings">
      <Card.Header title="Advance Settings"></Card.Header>
      <Card.Divider />
      <Card.Content>
        <Box direction="vertical" gap="22px">
          <Box direction="vertical" gap="19px">
            <Checkbox
              checked={state.setting.showAlertMessage}
              onChange={onChange("showAlertMessage")}
              className="anti-spy-cbx"
              size="small"
            >
              {getCheckboxLabel({
                label: "Show alert message",
                desc: "Alert text message for right click.",
              })}
            </Checkbox>
            <div className="anti-spy-advance-settings__alert">
              <Box direction="vertical" gap="8px">
                <FormField labelSize="small" label="Selection disabled message">
                  <Input
                    type="text"
                    className="anti-spy-advance-settings__alert__input"
                    disabled={!state.setting.showAlertMessage}
                    onChange={onChangeInput("selectionDisabledMessage")}
                    onBlur={onResetEvent}
                    size="medium"
                    value={state.setting.selectionDisabledMessage}
                    placeholder="Selection disabled message"
                  />
                </FormField>
                <FormField labelSize="small" label="Disappear after (second)">
                  <NumberInput
                    min={1}
                    max={10}
                    className="anti-spy-advance-settings__alert__input"
                    disabled={!state.setting.showAlertMessage}
                    onChange={onChangeInput("disappearAfterSeconds")}
                    onBlur={onBlurDissapear}
                    size="medium"
                    value={state.setting.disappearAfterSeconds}
                    placeholder="Disappear after (second)"
                    invalidMessage={
                      state.setting.disappearAfterSeconds > 0 &&
                      state.setting.disappearAfterSeconds < 11
                        ? ""
                        : "Enter input between 1 to 10."
                    }
                  />
                </FormField>
              </Box>
            </div>
          </Box>

          <Box direction="vertical" gap="19px">
            <Checkbox
              checked={state.setting.logLegalNotice}
              onChange={onChange("logLegalNotice")}
              className="anti-spy-cbx"
              size="small"
            >
              {getCheckboxLabel({
                label: "Log legal notice",
                desc: "Log legal notice to the developer console",
              })}
            </Checkbox>
            <div className="anti-spy-advance-settings__alert">
              <Box direction="vertical" gap="8px">
                <FormField labelSize="small" label="Legal header">
                  <Input
                    type="text"
                    className="anti-spy-advance-settings__alert__input"
                    disabled={!state.setting.logLegalNotice}
                    onChange={onChangeInput("legalHeader")}
                    size="medium"
                    value={state.setting.legalHeader}
                    placeholder="Legal header"
                  />
                </FormField>
                <FormField labelSize="small" label="Legal footer">
                  <Input
                    className="anti-spy-advance-settings__alert__input"
                    disabled={!state.setting.logLegalNotice}
                    onChange={onChangeInput("legalFooter")}
                    size="medium"
                    type="text"
                    value={state.setting.legalFooter}
                    placeholder="Legal footer"
                  />
                </FormField>
                <FormField labelSize="small" label="Legal content">
                  <InputArea
                    rows={3}
                    className="anti-spy-advance-settings__alert__input"
                    disabled={!state.setting.logLegalNotice}
                    onChange={onChangeInput("legalContent")}
                    size="medium"
                    value={state.setting.legalContent}
                    placeholder="Legal content"
                  />
                </FormField>
              </Box>
            </div>
          </Box>
        </Box>
      </Card.Content>
    </Card>
  );
};

export default AdvanceSetting;
