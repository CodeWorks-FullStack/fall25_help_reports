

namespace help_reports.Services;

public class ReportsService(ReportsRepository repo, RestaurantsService restaurantService)
{
  private readonly ReportsRepository _repo = repo;
  private readonly RestaurantsService _restaurantService = restaurantService;

  internal Report CreateReport(Report reportData)
  {
    Restaurant restaurant = _restaurantService.GetRestaurantById(reportData.RestaurantId, reportData.CreatorId);
    // restaurant isn't needed here because or GetRestaurantById already handles our checks but if there were more we could do them here
    Report report = _repo.CreateReport(reportData);
    return report;
  }

  internal List<Report> GetRestaurantReports(int restaurantId, string userId)
  {
    _restaurantService.GetRestaurantById(restaurantId, userId);
    List<Report> reports = _repo.GetRestaurantReports(restaurantId);
    return reports;
  }
}