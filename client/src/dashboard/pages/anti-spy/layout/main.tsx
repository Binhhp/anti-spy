import React from "react";
import { MainLayoutContainer } from "./main.styled";

export default function MainLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return <MainLayoutContainer>{children}</MainLayoutContainer>;
}
