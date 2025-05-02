import { Box, Card, Cell, Layout, Text } from "@wix/design-system";
import React, { FC } from "react";
import { AppPreviewContainer } from "./app-preview.styled";
import { seeding } from "./params";
import "site/embedded-scripts/anti-spy-scripts/styled.css";

const AppPreview: FC = () => {
  return (
    <AppPreviewContainer id="anti-spy-preview">
      <Card>
        <Card.Header title="Preview"></Card.Header>
        <Card.Divider></Card.Divider>
        <Card.Content size="medium">
          <Box direction="vertical" gap="25px">
            <Text size="small">Click the right mouse to see how it works</Text>
            <Layout gap="15px">
              {seeding.products.flatMap((p, index) => (
                <Cell span={6} key={`product-${index}`}>
                  <Box
                    style={{
                      cursor: "pointer",
                    }}
                    direction="vertical"
                    gap="10px"
                  >
                    <div className="anti-spy-preview-img">
                      <img src={p.thumnails} alt={p.title} />
                      {p.tag && (
                        <div className="anti-spy-preview-tag">
                          <Text size="small">{p.tag}</Text>
                        </div>
                      )}
                    </div>
                    <Text size="small">{p.title}</Text>
                    <Text size="small">{p.price}</Text>
                  </Box>
                </Cell>
              ))}
            </Layout>
          </Box>
        </Card.Content>
      </Card>
    </AppPreviewContainer>
  );
};

export default AppPreview;
