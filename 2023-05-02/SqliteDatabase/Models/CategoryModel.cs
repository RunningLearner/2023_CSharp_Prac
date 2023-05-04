using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Category;

[Table("Categories")]
public class CategoryModel
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string CategoryName { get; set; }
}
