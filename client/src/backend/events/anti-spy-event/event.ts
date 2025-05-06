import { appInstances } from "@wix/app-management";
import { apiExplorer } from "api/api-explorer";

appInstances.onAppInstanceInstalled(async (event) => {
  console.log(`App instance ${event.metadata.instanceId} installed`);
});

appInstances.onAppInstanceRemoved(async (event) => {
  console.log("App instance removed", event?.metadata?.instanceId);
  const instanceId = event.metadata.instanceId;
  if (!instanceId) return;
  console.log("Start call uninstall store");
  await apiExplorer.unInstallStore(instanceId);
});
