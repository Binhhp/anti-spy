import { Card, Text } from "@wix/design-system";
import React from "react";

export default function AppPreview() {
  return (
    <Card>
      <Card.Header title="Preview"></Card.Header>
      <Card.Divider></Card.Divider>
      <Card.Content size="medium">
        <Text>Preview</Text>
      </Card.Content>
    </Card>
  );
}
