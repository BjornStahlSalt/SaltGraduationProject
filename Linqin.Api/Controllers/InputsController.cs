using CodeRunner;
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
    public async Task<ActionResult<string>> ExecuteLinqQuery(LinqQuery linqQuery)
    {
        return await _codeRunnerService.RunLinqQueryOnList(linqQuery.ListDef, linqQuery.Query);
    }

}
