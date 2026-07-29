using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoOne.Controllers;
using TodoOne.Data;
using TodoOne.Models;
using Xunit;

namespace TodoOne.Tests;

public class TodoTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task Create_ThenGetAll_ReturnsTheItem()
    {
        await using var db = CreateContext();
        var controller = new TodoController(db);

        await controller.Create(new Todo { Text = "Buy coffee" });

        var result = await controller.GetAll();
        var items = Assert.IsAssignableFrom<IEnumerable<Todo>>(result.Value);

        Assert.Single(items);
        Assert.Equal("Buy coffee", items.First().Text);
    }

    [Fact]
    public async Task Delete_RemovesTheItem()
    {
        await using var db = CreateContext();
        var controller = new TodoController(db);
        db.Todos.Add(new Todo { Id = 1, Text = "Write CV" });
        await db.SaveChangesAsync();

        var result = await controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Empty(db.Todos);
    }
}