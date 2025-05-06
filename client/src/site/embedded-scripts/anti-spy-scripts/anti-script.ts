import { apiExplorer } from "backend/api/anti-spy-api/api";
import { events } from "./events";

(async function() {
  const siteId = window?.wixEmbedsAPI?.getMetaSiteId();
  console.log(`AntiSpy Embedded scripts enabled in ${siteId}.`);
  const resp = await apiExplorer.getBySiteIdAsync(siteId);
  if(!resp.success || !resp.data) {
    console.log(`Could not call get store by site.`);
    return;
  }
  const eventsProd = new events(resp.data?.settings);
  eventsProd.init();
})();