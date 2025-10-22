
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

    Restaurant createdRestaurant = _db.Query(
      sql,
      (Restaurant restaurant, Profile owner) =>
      {
        restaurant.Owner = owner;
        return restaurant;
      },
      restaurantData).SingleOrDefault();

    return createdRestaurant;
  }
}