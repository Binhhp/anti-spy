import { apiExplorer } from "backend/api/anti-spy-api/api";
import { events } from "./events";

(async function() {
  const siteId = window?.wixEmbedsAPI?.getMetaSiteId();
  const resp = await apiExplorer.getBySiteIdAsync(siteId);
  if(!resp.success || !resp.data) return;
  const eventsProd = new events(resp.data?.settings);
  eventsProd.init();
})();