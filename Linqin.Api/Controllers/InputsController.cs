using Linqin.Api.Models;
using Microsoft.AspNetCore.Mvc;
using LinqCompiler;

namespace Linqin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InputsController : ControllerBase
{

  [HttpPost]
  public async Task<ActionResult<ResponsePost>> ExecuteLinqQuery(RequestPost linqQuery)
  {
    try
    {
      return Ok(Compiler.ExecuteString(linqQuery.Query, linqQuery.listOfShapes));
    }
    catch (Exception ex)
    {
      return Ok(new ResponsePost() { ErrorMessage = ex.Message });
    }
    catch
    {
      return BadRequest();
    }
  }

}
