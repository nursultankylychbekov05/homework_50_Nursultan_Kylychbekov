using System.ComponentModel.DataAnnotations;

namespace homeWork_50.Models;

public class Brand
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название бренда")]
    [Display(Name = "Бренд")]
    public string Name { get; set; } = string.Empty;

    public List<Product> Products { get; set; } = new();
}