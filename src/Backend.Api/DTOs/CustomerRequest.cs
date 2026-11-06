using System.ComponentModel.DataAnnotations;

namespace Backend.Api.DTOs;

public record CustomerRequest(
    [Required, StringLength(100)]
    string Name,

    [Required, StringLength(50)]
    string Email,

    [Required, StringLength(15)]
    string Phone,

    [Required, StringLength(255)]
    string Address 
);