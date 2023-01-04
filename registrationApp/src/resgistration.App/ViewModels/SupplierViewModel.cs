
using System.ComponentModel.DataAnnotations;

namespace resgistration.App.ViewModels
{
    public class SupplierViewModel
    {
        [Key]
        public Guid id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracters", MinimumLength = 11)]
        public string Document { get; set; }

        [Display(Name = "Tipo")]
        public int SupplierType { get; set; }

        public AddressViewModel Address { get; set; }

        [Display(Name = "Ativo?")]
        public bool Active { get; set; }


        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
