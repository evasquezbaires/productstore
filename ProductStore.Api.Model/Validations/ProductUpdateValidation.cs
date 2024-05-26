using FluentValidation;

namespace ProductStore.Api.Model.Validations
{
    /// <summary>
    /// Configure the validation for the request to update products
    /// </summary>
    public class ProductUpdateValidation : AbstractValidator<ProductUpdate>
    {
        public ProductUpdateValidation()
        {
            RuleFor(rule => rule)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleFor(rule => rule.Id)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty();

                RuleFor(rule => rule.Name)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty();

                RuleFor(rule => rule.Description)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty();

                RuleFor(rule => rule.StatusCode)
                    .Cascade(CascadeMode.Stop)
                    .NotNull();

                RuleFor(rule => rule.Stock)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .GreaterThanOrEqualTo(0);

                RuleFor(rule => rule.Price)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .GreaterThan(0);
            });
        }
    }
}
