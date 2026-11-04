using System.ComponentModel.DataAnnotations;


public record ProductRequest
{
    [Required]
    [StringLength(20,MinimumLength =4)]
    public string ProductCode {get;set;} = string.Empty;
    [Required]
    public string Name {get;set;} = string.Empty;
    [Required]
    public decimal Price {get;set;}
    [Required]
    [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
    public int Stock {get;set;}
    [Required]
    public string Category {get;set;} = string.Empty;
}