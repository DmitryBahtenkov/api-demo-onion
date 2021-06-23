using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Выберите начало")]
        public DateTime StartAt { get; set; }
        [Required(ErrorMessage = "Выберите конец")]
        public DateTime FinishedAt { get; set; }
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пользователь не выбран")]
        public Guid UserId { get; set; }
    }
}