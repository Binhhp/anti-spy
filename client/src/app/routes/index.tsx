import { useRoutes } from "react-router-dom";
import MainLayout from "../layout/main";
import React from "react";
import AppSettings from "app/components/app-settings";

export default function Routes() {
  return useRoutes([
    {
      path: "/",
      element: <MainLayout />,
      children: [
        {
          path: "/",
          element: <AppSettings />,
        },
      ],
    },
  ]);
}
