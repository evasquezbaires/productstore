using FluentValidation;

namespace ProductStore.Api.Model.Validations
{
    /// <summary>
    /// Configure the validation for the request to search products
    /// </summary>
    public class ProductQueryValidation : AbstractValidator<ProductQuery>
    {
        public ProductQueryValidation()
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
            });
        }
    }
}
