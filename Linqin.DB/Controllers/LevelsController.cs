using Microsoft.AspNetCore.Mvc;
using Linqin.DB.Models;
using Linqin.DB.Data;


namespace Linqin.DB.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LevelsController : ControllerBase
  {
    private readonly LevelRepository _storage;

    public LevelsController()
    {
      _storage = new LevelRepository();
    }

    [HttpGet("{id}")]
    public ActionResult<GetResponse> GetLevel(string id)
    {
      var level = _storage.GetOne(id);

      if (level == null)
        return StatusCode(400);

      _storage.DeserializeLevel(level);
      return Ok(level);
    }

    [HttpGet]
    public ActionResult<List<GetResponse>> GetLevels()
    {
      var levels = _storage.GetAll();
      var listOfLevels = new List<GetResponse>();

    foreach (var level in levels)
    {
      if(level != null)
        listOfLevels.Add(_storage.DeserializeLevel(level));
    }

      return Ok(listOfLevels);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLevel(string id)
    {
      var level = _storage.GetOne(id);

      if (level == null)
        return StatusCode(400);

      _storage.Delete(id);
      return NoContent();
    }

    [HttpPut]
    public IActionResult UpdateLevel(string id, PostRequest request)
    {
      var level = _storage.GetOne(id);
      
      if (level == null)
        return StatusCode(400);

      _storage.UpdateData(id, request);
      return NoContent();
    }

    [HttpPost]
    public IActionResult CreateLevel(PostRequest request)
    {
      var levelId = _storage.AddData(request);
      return CreatedAtAction(nameof(GetLevel), new { id = levelId }, request);
    }
  }
}
