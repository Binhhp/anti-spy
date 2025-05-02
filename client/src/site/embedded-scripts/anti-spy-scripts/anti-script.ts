import { apiExplorer } from "apis/api";
import { events } from "./events";

(async () => {
  const siteId = window?.wixEmbedsAPI?.getMetaSiteId();
  const resp = await apiExplorer.getBySiteIdAsync(siteId);
  if(!resp.success || !resp.data) return;
  const eventsProd = new events(resp.data?.settings);
  eventsProd.initProtection();
})();