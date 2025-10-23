

namespace help_reports.Repositories;

public class ReportsRepository(IDbConnection db)
{
  private readonly IDbConnection _db = db;

  internal Report CreateReport(Report reportData)
  {
    string sql = @"
    INSERT INTO reports
    (title, body, score, img_url, creator_id, restaurant_id)
    VALUES
    (@Title, @Body, @Score, @ImgUrl, @CreatorId, @RestaurantId);

    SELECT 
    accounts.*,
    reports.*
    FROM reports
    INNER JOIN accounts ON accounts.id = reports.creator_id
    WHERE reports.id = LAST_INSERT_ID();
    ";
    Report report = _db.Query(
      sql,
      (Profile profile, Report report) =>
      {
        report.Creator = profile;
        return report;
      },
     reportData).SingleOrDefault();
    return report;
  }

  internal List<Report> GetRestaurantReports(int restaurantId)
  {
    string sql = @"
    SELECT 
      reports.*,
      accounts.*
    FROM reports
    INNER JOIN accounts ON accounts.id = reports.creator_id
    WHERE restaurant_id = @restaurantId
    ";
    List<Report> reports = _db.Query(
      sql,
      (Report report, Profile profile) =>
      {
        report.Creator = profile;
        return report;
      },
     new { restaurantId }).ToList();
    return reports;
  }
}