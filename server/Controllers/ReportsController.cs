namespace help_reports.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController(Auth0Provider auth, ReportsService reportsService) : ControllerBase
{
  private readonly ReportsService _reportsService = reportsService;
  private readonly Auth0Provider _auth = auth;


  [HttpPost]
  [Authorize]
  async public Task<ActionResult<Report>> CreateReport([FromBody] Report reportData)
  {
    try
    {
      Account account = await _auth.GetUserInfoAsync<Account>(HttpContext);
      reportData.CreatorId = account.Id;
      Report report = _reportsService.CreateReport(reportData);
      return Ok(report);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}