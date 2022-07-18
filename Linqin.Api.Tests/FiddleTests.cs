using Xunit;
using FluentAssertions;


namespace Linqin.Api.Tests;

public class FiddleTests // controller, coderunner, fiddleclient
{
    [Fact]
    public async void fiddle_returns_string()
    {
        // Arrange
        string listDef = "var test = int[]{7,2,8,3,6,3};";
        string query = "test.OrderBy(x => x);";
        var testService = new CodeRunner.CodeRunnerService();
        // Act
        var result = await testService.RunLinqQueryOnList(listDef, query);
        // Assert
        result.Should().Be("2,3,");
    }
}