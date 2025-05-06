import { apiExplorer } from "api/api-explorer";
import { events } from "./events";

(async function () {
  const siteId = window?.wixEmbedsAPI?.getMetaSiteId();
  const resp = await apiExplorer.getBySiteIdAsync(siteId);
  if (!resp.success || !resp.data) {
    console.log(`Could not call get store by site ${siteId}.`);
    return;
  }
  const eventsProd = new events(resp.data?.settings);
  eventsProd.init();
})();
