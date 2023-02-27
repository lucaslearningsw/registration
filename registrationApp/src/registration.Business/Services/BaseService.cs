using BasicMVC.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration.Business.Services
{
    public abstract class BaseService
    {
        protected void Notification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notification(error.ErrorMessage);
            }
        }


        protected void Notification(string message)
        {

        }

        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notification(validator);

            return false;
        }
    }

}
