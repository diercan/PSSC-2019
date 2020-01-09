using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ml_stats_core.Interfaces;
using ml_stats_core.Models;

namespace ml_stats_infrastructure.Services
{
  public class ExpDataHub : Hub<IExpDataHub>
  {
    public async Task SendToAll(PlotPoint entity)
    {
      await Clients.All.SendDataToAll(entity);
    }
  }
}