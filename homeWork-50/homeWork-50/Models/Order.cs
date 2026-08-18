using System.ComponentModel.DataAnnotations;

namespace homeWork_50.Models;

public class Order
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите ваше имя")]
    [Display(Name = "Имя покупателя")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите адрес доставки")]
    [Display(Name = "Адрес")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Укажите контактный телефон")]
    [Display(Name = "Телефон")]
    public string ContactPhone { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.Now;

    public int ProductId { get; set; }
    public Product? Product { get; set; }
}