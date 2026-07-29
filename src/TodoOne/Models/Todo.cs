using System.ComponentModel.DataAnnotations;

namespace TodoOne.Models;

public class Todo
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Text { get; set; } = string.Empty;

    public bool Done { get; set; }
}