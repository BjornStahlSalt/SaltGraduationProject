using Xunit;
using FluentAssertions;


namespace Linqin.Api.Tests;

public class FiddleTests // controller, coderunner, fiddleclient
{
    [Fact]
    public void fiddle_returns_string()
    {
        // Arrange
        string listDef = "var test = int[]{7,2,8,3,6,3};";
        string query = "test.OrderBy(x => x);";
        // Act
        var result = Linqin.Api.Services.CodeRunnerService.CodeRunner
        // Assert
    }
}