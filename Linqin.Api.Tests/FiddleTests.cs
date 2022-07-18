using Xunit;
using FluentAssertions;
using System;
using Linqin.Api.Models;
using System.Collections.Generic;

namespace Linqin.Api.Tests;

public class FiddleTests // controller, coderunner, fiddleclient
{

  private List<GeometryShapes> listOfShapes = new List<GeometryShapes>
    {
      new GeometryShapes { Shape = "Circle", Color = "Red", PriorityValue = 1},
      new GeometryShapes { Shape = "Square", Color = "Green", PriorityValue = 2},
      new GeometryShapes { Shape = "Triangle", Color = "Blue", PriorityValue = 3}
    };

  [Fact]
  public async void coderunnerservice_returns_string()
  {
    // Arrange
    string query = "test.OrderBy(x => x);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    var result = await testService.RunLinqQueryOnList(listOfShapes, query);
    // Assert
    result.Should().BeOfType(typeof(string));
  }

  [Fact]
  public async void coderunnerservice_returns_query_result()
  {
    // Arrange
    string query = "test.OrderBy(x => x);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    var result = await testService.RunLinqQueryOnList(listOfShapes, query);
    // Assert
    result.Should().Be("2, 3, 3, 6, 7, 8");
  }

  [Fact]
  public async void coderunnerservice_should_catch_exception()
  {
    // Arrange
    string query = "test.OrderBy(x => xfds);";
    var testService = new CodeRunner.CodeRunnerService();
    // Act
    var result = await testService.RunLinqQueryOnList(listOfShapes, query);
    // Assert
    result.Should().Be(" The name 'xfds' does not exist in the current context\r\n");
  }

  [Fact]
  public async void getlistasstring_should_return_stringified_list_of_shapes()
  {
    // Arrange
    var service = new CodeRunner.CodeRunnerService();

    // Act
    var result = service.GetListAsString(listOfShapes);
    // Assert
    result.Should().Be("new []{new {Shape = Circle, Color = Red, PriorityValue = 1}, new {Shape = Square, Color = Green, PriorityValue = 2}, new {Shape = Triangle, Color = Blue, PriorityValue = 3} };");
  }
}