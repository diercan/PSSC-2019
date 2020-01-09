using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using ml_stats_core.Exceptions;
using ml_stats_core.Interfaces;
using ml_stats_core.Models;

namespace ml_stats_webapp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExperimentsController : ControllerBase
  {
    private readonly IExperimentItemRepository _experimentRepository;
    private readonly IPlotItemRepository _plotRepository;

    public ExperimentsController(IExperimentItemRepository experimentRepository, IPlotItemRepository plotRepository)
    {
      _experimentRepository = experimentRepository;
      _plotRepository = plotRepository;
    }
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult> CreateItem([FromBody] Experiment newExperiment)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      newExperiment.CreatedAt = DateTime.UtcNow;
      foreach (var plt in newExperiment.ListOfPlots)
      {
        await _plotRepository.AddAsync(plt);
      }
      var exp = await _experimentRepository.AddAsync(newExperiment);

      return Ok(exp);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("plots")]
    public async Task<ActionResult> CreateItemPlot([FromBody] Plot newPlot)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      
      var plt = await _plotRepository.AddAsync(newPlot);

      return Ok(plt);
    }

    [HttpGet("{expId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Experiment))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetItem(string expId)
    {
      try
      {
        var exp = await _experimentRepository.GetByIdAsync(expId);
        return Ok(exp);
      }
      catch (EntityNotFoundException)
      {
        return NotFound(expId);
      }
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetItems()
    {
      try
      {
        string sql = $"SELECT * FROM s";
        var exp = await _experimentRepository.GetByExpressionAsync(sql, new SqlParameterCollection());
        return Ok(exp);
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}