import { appInstances } from "@wix/app-management";
import { apiExplorer } from "backend/api/anti-spy-api/api";

appInstances.onAppInstanceRemoved(async (event) => {
  console.log("App instance removed", event?.metadata?.instanceId);
  const instanceId = event.metadata.instanceId;
  if(!instanceId) return;
  console.log("Start call uninstall store");
  await apiExplorer.unInstallStore(instanceId);
});
