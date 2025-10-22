using System.ComponentModel.DataAnnotations;

namespace help_reports.Models;

public class Restaurant : RepoItem<int>
{
  public string Name { get; set; }
  [Url] public string ImgUrl { get; set; }
  public string Description { get; set; }
  // TODO make sure this is actually implemented in the repository
  public int Visits { get; set; }
  public bool? IsShutdown { get; set; }
  public string CreatorId { get; set; }
  public Profile Owner { get; set; }
  // TODO make sure this is actually implemented in the repository
  public int ReportCount { get; set; }
}