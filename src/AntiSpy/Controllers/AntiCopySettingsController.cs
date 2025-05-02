using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class AntiCopySettingsController(UnitOfWork _unit) : ControllerBase
{
    [HttpGet]
    [Route("stores/{instanceId}")]
    public IActionResult Get([FromRoute] string instanceId)
    {
        var settings = _unit.AntiCopySettings.Get(instanceId);
        return Ok(settings);
    }

    [HttpPost]
    [Route("stores/{instanceId}")]
    public async Task<IActionResult> Set([FromRoute] string instanceId, [FromBody] AntiCopySettingsRequest request)
    {
        var resposne = await _unit.AntiCopySettings.Set(instanceId, request);
        return Ok(resposne);
    }

    [HttpGet]
    [Route("sites/{siteId}")]
    public IActionResult GetSettingBySiteId([FromRoute] string siteId)
    {
        var store = _unit.AntiCopySettings.GetBySiteId(siteId);
        return Ok(store);
    }
}
