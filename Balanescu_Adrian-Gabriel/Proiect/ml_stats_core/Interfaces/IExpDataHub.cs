using System.Threading.Tasks;
using ml_stats_core.Models;

namespace ml_stats_core.Interfaces
{
  public interface IExpDataHub
  {
    Task SendDataToAll(PlotPoint entity);
  }
}