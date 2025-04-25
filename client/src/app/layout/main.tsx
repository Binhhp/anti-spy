import React from "react";
import { MainLayoutContainer } from "./main.styled";
import { Outlet } from "react-router-dom";

export default function MainLayout() {
  return (
    <MainLayoutContainer>
      <Outlet />
    </MainLayoutContainer>
  );
}
