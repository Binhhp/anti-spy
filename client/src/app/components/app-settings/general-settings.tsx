import { Card, Checkbox } from "@wix/design-system";
import React from "react";

export default function GeneralSettings() {
  return (
    <Card>
      <Card.Header title="General Settings"></Card.Header>
      <Card.Divider />
      <Card.Content>
        <div className="anti-spy-general-settings">
          <Checkbox size="medium">Medium</Checkbox>
          <Checkbox size="small">Small</Checkbox>
        </div>
      </Card.Content>
    </Card>
  );
}
