using System.ComponentModel.DataAnnotations;

namespace homeWork_50.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите наименование товара")]
    [Display(Name = "Наименование")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите стоимость")]
    [Range(0.01, 100000000, ErrorMessage = "Укажите корректную цену")]
    [Display(Name = "Стоимость")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Укажите ссылку на изображение")]
    [Display(Name = "Ссылка на изображение")]
    public string ImageUrl { get; set; } = string.Empty;

    [Display(Name = "Дата создания")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Display(Name = "Дата обновления")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Выберите категорию")]
    [Display(Name = "Категория")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Required(ErrorMessage = "Выберите бренд")]
    [Display(Name = "Бренд")]
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }
}