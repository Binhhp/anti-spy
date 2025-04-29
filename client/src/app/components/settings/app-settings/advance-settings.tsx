import { Card, Checkbox } from "@wix/design-system";
import React from "react";

export default function AdvanceSetting() {
  return (
    <Card>
      <Card.Header title="Advance Settings"></Card.Header>
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
