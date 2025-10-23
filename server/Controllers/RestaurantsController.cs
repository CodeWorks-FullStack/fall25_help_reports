using System.Threading.Tasks;

namespace help_reports.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  private readonly RestaurantsService _restaurantsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly ReportsService _reportsService;

  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth0Provider, ReportsService reportsService)
  {
    _restaurantsService = restaurantsService;
    _auth0Provider = auth0Provider;
    _reportsService = reportsService;
  }

  [Authorize, HttpPost]
  public async Task<ActionResult<Restaurant>> CreateRestaurant([FromBody] Restaurant restaurantData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      restaurantData.CreatorId = userInfo.Id;
      restaurantData.IsShutdown ??= false;
      Restaurant restaurant = _restaurantsService.CreateRestaurant(restaurantData);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetAllRestaurants()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Restaurant> restaurants = _restaurantsService.GetAllRestaurants(userInfo?.Id);
      return Ok(restaurants);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> GetRestaurantById(int restaurantId)
  {
    try
    {
      // NOTE you can still try to see who is logged in without authorizing the route
      // NOTE userInfo will be null if the user is not logged in
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _restaurantsService.GetRestaurantById(restaurantId, userInfo?.Id);
      return restaurant;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize, HttpPut("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> UpdateRestaurant(int restaurantId, [FromBody] Restaurant restaurantUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant updatedRestaurant = _restaurantsService.UpdateRestaurant(restaurantId, userInfo.Id, restaurantUpdateData);
      return Ok(updatedRestaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize, HttpDelete("{restaurantId}")]
  public async Task<ActionResult<string>> DeleteRestaurant(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _restaurantsService.DeleteRestaurant(restaurantId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{restaurantId}/reports")]
  async public Task<ActionResult<List<Report>>> GetRestaurantReports(int restaurantId)
  {
    try
    {
      Account account = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext); // Does not error even if you are not logged in. *Just makes the account variable null*
      // NOTE if the account is null, you cannot access .Id, ERROR:(Object reference not set to an instance of an object)
      List<Report> reports = _reportsService.GetRestaurantReports(restaurantId, account?.Id);
      return Ok(reports);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}