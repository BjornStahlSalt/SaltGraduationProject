using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using Linqin.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqCompiler;
using System.Threading;

namespace Linqin.Api.Tests;

public class CSharpCompilerTests // controller, coderunner, fiddleclient
{

  private List<ShapeModel> listOfShapes = new List<ShapeModel>
    {
      new ShapeModel { Shape = "Circle", Color = "Red", PriorityValue = 1},
      new ShapeModel { Shape = "Square", Color = "Green", PriorityValue = 3},
      new ShapeModel { Shape = "Square", Color = "Blue", PriorityValue = 3},
      new ShapeModel { Shape = "Triangle", Color = "Green", PriorityValue = 2},
      new ShapeModel { Shape = "Square", Color = "Red", PriorityValue = 3},
      new ShapeModel { Shape = "Triangle", Color = "Green", PriorityValue = 2}
    };

  // [Fact]
  // public async void executestring_returns_ResponsePost()
  // {
  //   // Arrange
  //   string query = ".OrderBy(x => x.PriorityValue);";
  //   // Act
  //   var result = Compiler.ExecuteString(query, listOfShapes);
  //   // Assert
  //   result.Should().BeOfType(typeof(ResponsePost));
  // }

  [Theory]
  [InlineData("OrderBy(x => x.PriorityValue);", new int[] { 1, 2, 2, 3, 3, 3 })]
  [InlineData("OrderBy(x => x.PriorityValue).Reverse();", new int[] { 3, 3, 3, 2, 2, 1 })]
  [InlineData("OrderByDescending(x => x.PriorityValue);", new int[] { 3, 3, 3, 2, 2, 1 })]
  [InlineData("Where(x => x.Color == \"Red\");", new int[] { 1, 3 })]
  [InlineData("Where(x => x.Color == \"Green\");", new int[] { 3, 2 })]
  [InlineData("Where(x => x.Color == \"Green\" || x.Color == \"Blue\");", new int[] { 3, 3, 2, 2 })]
  [InlineData("TakeWhile(x => x.Shape != \"Triangle\");", new int[] { 1, 3, 3 })]
  [InlineData("Take(3);", new int[] { 1, 3, 3 })]
  [InlineData("Skip(2);", new int[] { 3, 2, 3, 2 })]
  [InlineData("TakeLast(2);", new int[] { 3, 2 })]
  [InlineData("Distinct();", new int[] { 1, 3, 3, 2, 3 })]
  [InlineData("OrderBy(x => x.PriorityValue).Reverse().Take(4).Where(x => x.Color != \"Blue\");", new int[] { 3, 3 })]
  public async void executestring_returns_query_result(string query, int[] expected)
  {
    // Act
    var result = Compiler.ExecuteString(query, listOfShapes);
    // Assert;
    (result.ListOfShapes as IEnumerable<ShapeModel>).Select(s => s.PriorityValue).Should().ContainInOrder(expected);
  }

  // [Fact]
  // public async void executestring_should_catch_exception()
  // {
  //   // Arrange
  //   string query = ".OrderBy(x => xfds);";

  //   // Act
  //   var result = Compiler.ExecuteString(query, listOfShapes);

  //   // Assert
  //   result.ErrorMessage.Should().NotBeNull();
  // }

  [Fact]
  public async void same_properties_should_be_equal()
  {
    // Arrange
    var array = new List<ShapeModel>(){
      new ShapeModel { Shape = "Square", Color = "Green", PriorityValue = 3},
      new ShapeModel { Shape = "Square", Color = "Green", PriorityValue = 3}
    };
    // Act
    ShapeModel mod1 = array[0];
    ShapeModel mod2 = array[1];
    bool result = ShapeModel.Equals(mod1, mod2);

    // Assert
    result.Should().Be(true);
  }

  // [Theory]
  // [InlineData(0)]
  // [InlineData(10)]
  // [InlineData(50)]
  // [InlineData(150)]
  // [InlineData(195)]
  // public void should_return_yes(int sleepTime)
  // {
  //   // Act
  //   var result = Compiler.CreateThread(() => { Thread.Sleep(sleepTime); return "Yes"; }) as string;

  //   // Assert
  //   result.Should().Be("Yes");
  // }

  // [Theory]
  // [InlineData(200)]
  // [InlineData(205)]
  // [InlineData(500)]
  // public async void should_throw_error(int sleepTime)
  // {

  //   // Act
  //   Action action = () => { var result = Compiler.CreateThread(() => { Thread.Sleep(sleepTime); return "Yes"; }); };

  //   // Assert
  //   action.Should().Throw<Exception>().WithMessage("Query ran for too long.");
  // }


  [Fact]
  public async void executestring_should_stop_thread()
  {
    // Arrange
    string query = "Where(s => {while(true){}return true;});";

    // Act
    Action action = () => { Compiler.ExecuteString(query, listOfShapes); };

    // Assert
    action.Should().Throw<Exception>().WithMessage("Query ran for too long.");
  }

}