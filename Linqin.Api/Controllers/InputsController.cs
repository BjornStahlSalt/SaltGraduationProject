using Linqin.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Linqin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InputsController : ControllerBase
{
  public InputsController()
  {

  }

  [HttpPost]
  public ActionResult<List<GeometryShapes>> ExecuteQuery(string query, List<GeometryShapes> inputCollection)
  {
    query = "inputCollection.Where(s => s.Color == Color.Red)"
    throw new NotImplementedException();
  }

}