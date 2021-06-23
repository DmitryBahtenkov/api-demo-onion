using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Dtos
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Пользователь не указан")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Введите старый пароль")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }
    }
}