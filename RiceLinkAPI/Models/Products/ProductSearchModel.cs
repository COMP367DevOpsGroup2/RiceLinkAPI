using System.ComponentModel.DataAnnotations;

namespace RiceLinkAPI.Models.Products
{
    public class ProductSearchModel
    {
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Name must be alphanumeric.")]
        public string Name { get; set; }
    }

}
