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

<<<<<<< HEAD
    [HttpPost]
    public async Task<ActionResult<string>> ExecuteLinqQuery(LinqQuery linqQuery)
    {
        return await _codeRunnerService.RunLinqQueryOnList(linqQuery.ListDef, linqQuery.Query);
    }
=======
  [HttpPost]
  public async Task<ActionResult<List<ShapeModel>>> ExecuteLinqQuery(RequestPost linqQuery)
  {
    try
    {
      return Ok(await _codeRunnerService.RunLinqQueryOnList(linqQuery.listOfShapes, linqQuery.Query));
    }
    catch (FiddleClientError ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
    catch
    {
      return BadRequest();
    }
  }
>>>>>>> Linqin.FrontEnd

}
