import { useRoutes } from "react-router-dom";
import MainLayout from "../layout/main";
import DashboardPage from "../pages/dashboard";
import React from "react";

export default function Routes() {
  return useRoutes([
    {
      path: "/",
      element: <MainLayout />,
      children: [
        {
          path: "/",
          element: <DashboardPage />,
        },
      ],
    },
  ]);
}
