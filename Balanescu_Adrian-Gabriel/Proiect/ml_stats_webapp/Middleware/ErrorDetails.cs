using Newtonsoft.Json;

namespace ml_stats_webapp.Middleware
{
  public class ErrorDetails
  {
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string ErrorType { get; set; }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}