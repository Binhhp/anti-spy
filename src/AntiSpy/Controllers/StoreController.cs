using Microsoft.AspNetCore.Mvc;

public class StoreController(UnitOfWork _unit) : ControllerBase
{
    [HttpGet]
    [Route("stores/{instanceId}")]
    public IActionResult Get([FromRoute] string instanceId)
    {
        var settings = _unit.Settings.Get(instanceId);
        return Ok(settings);
    }

    [HttpPost]
    [Route("stores/{instanceId}")]
    public async Task<IActionResult> Set([FromRoute] string instanceId, [FromBody] SettingsRequest request)
    {
        var resposne = await _unit.Settings.Set(instanceId, request);
        return Ok(resposne);
    }

    [HttpGet]
    [Route("sites/{siteId}")]
    public IActionResult GetSettingBySiteId([FromRoute] string siteId)
    {
        var store = _unit.Settings.GetBySiteId(siteId);
        return Ok(store);
    }

    [HttpGet]
    [Route("stores/{instanceId}/embedded-scripts")]
    public async Task<IActionResult> EmbeddedScripts([FromRoute] string instanceId)
    {
        var embeddedScripts = await _unit.Store.EmbeddedScripts(instanceId);
        return Ok(embeddedScripts);
    }

    [HttpDelete]
    [Route("stores/{instanceId}/uninstall")]
    public async Task<IActionResult> UnInstallStore([FromRoute] string instanceId)
    {
        var response = await _unit.Store.UninstallAsync(instanceId);
        return Ok(response);
    }
}
