


namespace help_reports.Repositories;

public class RestaurantsRepository
{
  private readonly IDbConnection _db;

  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Restaurant CreateRestaurant(Restaurant restaurantData)
  {
    string sql = @"
    INSERT INTO
    restaurants(name, description, img_url, is_shutdown, creator_id)
    VALUES(@Name, @Description, @ImgUrl, @IsShutdown, @CreatorId);

    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    WHERE restaurants.id = LAST_INSERT_ID();";

    // Restaurant createdRestaurant = _db.Query(
    //   sql,
    //   (Restaurant restaurant, Profile owner) =>
    //   {
    //     restaurant.Owner = owner;
    //     return restaurant;
    //   },
    //   restaurantData).SingleOrDefault();
    Restaurant createdRestaurant = _db.Query<Restaurant, Profile, Restaurant>(sql, JoinOwner, restaurantData).SingleOrDefault();

    return createdRestaurant;
  }

  internal List<Restaurant> GetAllRestaurants()
  {
    string sql = @"
    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    ORDER BY restaurants.created_at ASC;";

    List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>(sql, JoinOwner).ToList();

    return restaurants;
  }

  internal Restaurant GetRestaurantById(int restaurantId)
  {
    string sql = @"
    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    WHERE restaurants.id = @restaurantId;";

    Restaurant restaurant = _db.Query<Restaurant, Profile, Restaurant>(sql, JoinOwner, new { restaurantId }).SingleOrDefault();

    return restaurant;
  }

  private static Restaurant JoinOwner(Restaurant restaurant, Profile owner)
  {
    restaurant.Owner = owner;
    return restaurant;
  }
}