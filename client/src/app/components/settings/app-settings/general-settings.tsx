import { Box, Card, Checkbox, Text } from "@wix/design-system";
import React from "react";
import { stateContext } from "../state-manager";

export default function GeneralSettings() {
  const state = React.useContext(stateContext);

  const onChange =
    (key: "protectImages" | "protectText" | "stopKeyboardShortcuts") => () => {
      state.setting[key] = !state.setting[key];
      state.setState(state.setting);
    };
  const getCheckboxLabel = (item: {
    label: string;
    desc: string;
    caption?: string;
  }) => (
    <Box direction="vertical">
      <Text>{item.label}</Text>
      <Text size="tiny" secondary>
        {item.desc}
      </Text>
      {item.caption && (
        <Text size="tiny" secondary>
          {item.caption}
        </Text>
      )}
    </Box>
  );
  return (
    <Card>
      <Card.Header title="General Settings"></Card.Header>
      <Card.Divider />
      <Card.Content>
        <Box direction="vertical" gap="14px">
          <Checkbox
            checked={state.setting.protectImages}
            onChange={onChange("protectImages")}
            className="anti-spy-cbx"
            size="small"
          >
            {getCheckboxLabel({
              label: "Protect Images",
              desc: "Disable shortcuts to save images.",
            })}
          </Checkbox>
          <Checkbox
            className="anti-spy-cbx"
            size="small"
            checked={state.setting.protectText}
            onChange={onChange("protectText")}
          >
            {getCheckboxLabel({
              label: "Protect Text",
              desc: "Disable text highlighting and copy/paste",
            })}
          </Checkbox>
          <Checkbox
            className="anti-spy-cbx"
            size="small"
            checked={state.setting.stopKeyboardShortcuts}
            onChange={onChange("stopKeyboardShortcuts")}
          >
            {getCheckboxLabel({
              label: "Stop keyboard shortcuts",
              desc: "Prevent keyboard shortcuts to save content.",
              caption: "n (ctr + C, ctr + X, ctr + S included)",
            })}
          </Checkbox>
        </Box>
      </Card.Content>
    </Card>
  );
}
