

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace resgistration.App.ViewModels
{
    public class ProductViewModel
    {
        [Key]
        public Guid id { get; set; }

        public Guid SupplierId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Description { get; set; }

        
       // public IFormFile ImagemUpload { get; set; }

        public string Image { get; set; }
        public decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public DateTime RegistrationDate { get; set; }

        [DisplayName("Ativo?")]
        public bool Active { get; set; }

        public IEnumerable<SupplierViewModel> Suppliers { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}
