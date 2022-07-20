using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using Linqin.Api.Models;
using System.Collections.Generic;
using Fiddle.Exceptions;
using System.Threading.Tasks;


namespace Linqin.Api.Tests;

public class FiddleTests // controller, coderunner, fiddleclient
{

  private List<ShapeModel> listOfShapes = new List<ShapeModel>
    {
<<<<<<< HEAD
        // Arrange
        string listDef = "var test = int[]{7,2,8,3,6,3};";
        string query = "test.OrderBy(x => x);";
        // Act
        var result = Linqin.Api.Services.CodeRunnerService.CodeRunner
        // Assert
    }
=======
      new ShapeModel { Shape = "Circle", Color = "Red", PriorityValue = 2},
      new ShapeModel { Shape = "Square", Color = "Green", PriorityValue = 3},
      new ShapeModel { Shape = "Triangle", Color = "Blue", PriorityValue = 1}
    };

  [Fact]
  public async void coderunnerservice_returns_string()
  {
    // Arrange
    string query = "test.OrderBy(x => x.PriorityValue);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    var result = await testService.RunLinqQueryOnList(listOfShapes, query);
    // Assert
    result.Should().BeOfType(typeof(List<ShapeModel>));
  }

  [Fact]
  public async void coderunnerservice_returns_query_result()
  {
    // Arrange
    string query = "test.OrderBy(x => x.PriorityValue);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    var result = await testService.RunLinqQueryOnList(listOfShapes, query);
    // Assert
    result.Should().HaveCount(3);
    result.Select(s => s.PriorityValue).Should().ContainInOrder(new int[] { 1, 2, 3 });
  }

  [Fact]
  public async void coderunnerservice_should_catch_exception()
  {
    // Arrange
    string query = "test.OrderBy(x => xfds);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    Func<Task> action = async () => { await testService.RunLinqQueryOnList(listOfShapes, query); };

    // Assert
    action.Should().ThrowAsync<FiddleClientError>(" The name 'xfds' does not exist in the current context\r\n");
    //action.Should().Be(" The name 'xfds' does not exist in the current context\r\n");
  }

  [Fact]
  public async void getlistasstring_should_return_stringified_list_of_shapes()
  {
    // Arrange
    var service = new CodeRunner.CodeRunnerService();

    // Act
    var result = service.GetListAsString(listOfShapes);
    // Assert
    result.Should().Be("var test = new []{new {Shape = \"Circle\", Color = \"Red\", PriorityValue = 2}, new {Shape = \"Square\", Color = \"Green\", PriorityValue = 3}, new {Shape = \"Triangle\", Color = \"Blue\", PriorityValue = 1} };");
  }
>>>>>>> Linqin.FrontEnd
}