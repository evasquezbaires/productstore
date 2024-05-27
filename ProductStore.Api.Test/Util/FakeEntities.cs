using Bogus;
using ProductStore.Api.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Util
{
    [ExcludeFromCodeCoverage]
    public static class FakeEntities
    {
        public static List<Product> GetFakeProducts(int count)
        {
            return new Faker<Product>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Code, f => f.Commerce.Ean13())
                .RuleFor(u => u.Stock, f => f.Random.Number(1000))
                .RuleFor(u => u.Description, f => f.Random.AlphaNumeric(100))
                .RuleFor(u => u.Price, f => f.Random.Double())
                .RuleFor(u => u.StatusCode, f => f.Random.Int(0, 1))
                .RuleFor(u => u.CreatedDate, f => f.Date.Recent(30))
                .RuleFor(u => u.UpdatedDate, f => f.Date.Recent(10))
                .Generate(count);
        }

        public static Product GetFakeProduct()
        {
            return GetFakeProducts(1)[0];
        }

        public static Product GetFakeProduct(Guid id)
        {
            var fakeEntity = GetFakeProducts(1)[0];
            fakeEntity.Id = id;

            return fakeEntity;
        }
    }
}
