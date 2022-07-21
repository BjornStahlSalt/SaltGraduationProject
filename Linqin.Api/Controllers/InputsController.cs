using CodeRunner;
using Fiddle.Exceptions;
using Linqin.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Linqin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InputsController : ControllerBase
{
  private readonly CodeRunnerService _codeRunnerService;
  public InputsController(CodeRunnerService codeRunnerService)
  {
    _codeRunnerService = codeRunnerService;
  }

  [HttpPost]
  public async Task<ActionResult<ResponsePost>> ExecuteLinqQuery(RequestPost linqQuery)
  {
    try
    {
      return Ok(RoslynCore.EmitDemo.ExecuteString(linqQuery.Query, linqQuery.listOfShapes));
      // return Ok(new ResponsePost() { listOfShapes = await _codeRunnerService.RunLinqQueryOnList(linqQuery.listOfShapes, linqQuery.Query) });
    }
    // catch (FiddleClientError ex)
    // {
    //     return Ok(new ResponsePost() { ErrorMessage = ex.Message });
    // }
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
