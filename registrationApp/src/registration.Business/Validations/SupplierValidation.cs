using BasicMVC.Models;
using FluentValidation;
using registration.Business.Validations.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Business.Validations
{
    public class SupplierValidation : AbstractValidator<Supplier>
    {
        public SupplierValidation()
        {
            RuleFor(s => s.Name)
            .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
            .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(s => s.SupplierType == SupplierType.PhysicalPerson, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CpfValidacao.LengthCpf)
               .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(s => CpfValidacao.Validate(s.Document)).Equal(true)
                .WithMessage("O documento fornecido é inválido.");
            });

            When(s => s.SupplierType == SupplierType.LegalPerson, () =>
            {
                RuleFor(s => s.Document.Length).Equal(CnpjValidacao.TamanhoCnpj)
               .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                RuleFor(s => CnpjValidacao.Validar(s.Document)).Equal(true)
               .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}
