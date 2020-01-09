using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using ml_stats_core.Exceptions;
using ml_stats_core.Interfaces;
using User = ml_stats_core.Models.User;

namespace ml_stats_webapp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly IUserItemRepository _userRepository;

    public UsersController(IUserItemRepository userRepository)
    {
      _userRepository = userRepository;
    }
    
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult> CreateItem([FromBody] User newUser)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      /*
      string sql = $"SELECT * FROM s WHERE s.userName='{newUser.UserName}'";
      if (await _userRepository.GetOneByExpressionAsync(sql, new SqlParameterCollection()) != null)
      {
        return BadRequest("User already exists");
      }*/
      
      var user = await _userRepository.AddAsync(newUser);

      return Ok(user);
    }

    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetItem(string userId)
    {
      try
      {
        var user = await _userRepository.GetByIdAsync(userId);
        return Ok(user);
      }
      catch (EntityNotFoundException)
      {
        return NotFound(userId);
      }
    }
  }
}