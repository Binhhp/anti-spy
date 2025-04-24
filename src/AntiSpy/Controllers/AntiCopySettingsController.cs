using Microsoft.AspNetCore.Mvc;

public class AntiCopySettingsController(UnitOfWork _unit) : ControllerBase
{
    [HttpGet("stores/{instanceId}")]
    public IActionResult Get([FromRoute] string instanceId)
    {
        var settings = _unit.AntiCopySettings.Get(instanceId);
        return Ok(settings);
    }

    [HttpPost("stores/{instanceId}")]
    public async Task<IActionResult> Set([FromRoute] string instanceId, [FromBody] AntiCopySettingsRequest request)
    {
        await _unit.AntiCopySettings.Set(instanceId, request);
        return Ok();
    }
}
