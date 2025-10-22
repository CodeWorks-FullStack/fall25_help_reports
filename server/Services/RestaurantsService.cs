


namespace help_reports.Services;

public class RestaurantsService
{
  private readonly RestaurantsRepository _repository;

  public RestaurantsService(RestaurantsRepository repository)
  {
    _repository = repository;
  }

  internal Restaurant CreateRestaurant(Restaurant restaurantData)
  {
    Restaurant restaurant = _repository.CreateRestaurant(restaurantData);
    return restaurant;
  }

  internal string DeleteRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurantToDelete = GetRestaurantById(restaurantId);

    if (restaurantToDelete.CreatorId != userId)
    {
      throw new Exception("YOU CANNOT DELETE ANOTHER USER'S RESTAURANT. MUSHY MICK IS GOING TO ARREST YOU AND YOUR FAMILY");
    }

    _repository.DeleteRestaurant(restaurantId);

    return $"{restaurantToDelete.Name} has been deleted!";
  }

  internal List<Restaurant> GetAllRestaurants()
  {
    List<Restaurant> restaurants = _repository.GetAllRestaurants();
    return restaurants;
  }

  internal Restaurant GetRestaurantById(int restaurantId)
  {
    Restaurant restaurant = _repository.GetRestaurantById(restaurantId);
    if (restaurant == null) throw new Exception("No restaurant found with the id of: " + restaurantId);
    return restaurant;
  }

  internal Restaurant UpdateRestaurant(int restaurantId, string userId, Restaurant restaurantUpdateData)
  {
    Restaurant originalRestaurant = GetRestaurantById(restaurantId);

    if (originalRestaurant.CreatorId != userId)
    {
      throw new Exception("YOU CANNOT UPDATE ANOTHER USER'S RESTAURANT, MUSHY MICK IS NOW HIDING UNDER YOUR BED");
    }

    originalRestaurant.Description = restaurantUpdateData.Description ?? originalRestaurant.Description;
    originalRestaurant.ImgUrl = restaurantUpdateData.ImgUrl ?? originalRestaurant.ImgUrl;
    originalRestaurant.Name = restaurantUpdateData.Name ?? originalRestaurant.Name;
    originalRestaurant.IsShutdown = restaurantUpdateData.IsShutdown ?? originalRestaurant.IsShutdown;

    _repository.UpdateRestaurant(originalRestaurant);

    return originalRestaurant;
  }
}