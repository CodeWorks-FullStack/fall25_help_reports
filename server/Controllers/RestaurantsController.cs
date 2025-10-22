using System.Threading.Tasks;

namespace help_reports.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  private readonly RestaurantsService _restaurantsService;
  private readonly Auth0Provider _auth0Provider;

  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth0Provider)
  {
    _restaurantsService = restaurantsService;
    _auth0Provider = auth0Provider;
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
  public ActionResult<List<Restaurant>> GetAllRestaurants()
  {
    try
    {
      List<Restaurant> restaurants = _restaurantsService.GetAllRestaurants();
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
}