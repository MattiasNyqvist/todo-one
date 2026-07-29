using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoOne.Data;
using TodoOne.Models;

namespace TodoOne.Pages;

public class TodosModel : PageModel
{
    private readonly AppDbContext _db;

    public TodosModel(AppDbContext db)
    {
        _db = db;
    }

    public IReadOnlyList<Todo> Items { get; private set; } = [];

    [BindProperty]
    public string? NewText { get; set; }

    public async Task OnGetAsync()
    {
        Items = await _db.Todos.AsNoTracking().OrderBy(t => t.Id).ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (!string.IsNullOrWhiteSpace(NewText))
        {
            _db.Todos.Add(new Todo { Text = NewText.Trim() });
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToggleAsync(int id)
    {
        var todo = await _db.Todos.FindAsync(id);

        if (todo is not null)
        {
            todo.Done = !todo.Done;
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var todo = await _db.Todos.FindAsync(id);

        if (todo is not null)
        {
            _db.Todos.Remove(todo);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}