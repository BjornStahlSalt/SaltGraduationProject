using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using Linqin.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqCompiler;


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

  [Fact]
  public void executestring_returns_ResponsePost()
  {
    // Arrange
    string query = "OrderBy(x => x.PriorityValue);";
    // Act
    var result = Compiler.ExecuteString(query, listOfShapes);
    // Assert
    result.Should().BeOfType(typeof(ResponsePost));
  }

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
  public void executestring_returns_query_result(string query, int[] expected)
  {
    // Act
    var result = Compiler.ExecuteString(query, listOfShapes);
    // Assert;
    (result.ListOfShapes as IEnumerable<ShapeModel>).Select(s => s.PriorityValue).Should().ContainInOrder(expected);
  }

  [Fact]
  public void executestring_should_catch_exception()
  {
    // Arrange
    string query = ".OrderBy(x => xfds);";

    // Act
    var result = Compiler.ExecuteString(query, listOfShapes);

    // Assert
    result.ErrorMessage.Should().NotBeNull();
  }

  [Fact]
  public void same_properties_should_be_equal()
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

  [Theory]
  [InlineData("Where(x => {while(true){} return false;});")]
  [InlineData("Where(x => {for(int i = 0; i < 1000000000; i++){} return false;});")]
  [InlineData("Where(x => {var t = Enumerable.Range(1,100);.ToList().ForEach(() =>{}) return false;});")]
  [InlineData("Where(x => {var t = forEach(int i int int[]{1,2}){} return false;});")]
  [InlineData("Where(x => {var t = var t = List<int>{1,2}.ForEach(()=>{}) return false;});")]
  [InlineData("What is this gibberish_dll")]
  [InlineData("kadasForEachlajd")]
  [InlineData("hlhad.MaxValuealk")]
  [InlineData("hlhad.MinValuealk")]
  [InlineData("hlhadInfinityalk")]
  public void should_throw_for_dangerous_code(string query)
  {
    // Act
    Action action = () => Compiler.ExecuteString(query, listOfShapes);
    // Assert
    action.Should().Throw<Exception>().WithMessage("Something went wrong.");
  }


}