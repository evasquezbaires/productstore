using Bogus;
using ProductStore.Api.Model;
using ProductStore.Api.Model.Enum;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Test.Util
{
    [ExcludeFromCodeCoverage]
    public static class FakeModels
    {
        public static List<ProductWrite> GetFakeProductsWrite(int count)
        {
            return new Faker<ProductWrite>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Code, f => f.Commerce.Ean13())
                .RuleFor(u => u.Stock, f => f.Random.Number(1000))
                .RuleFor(u => u.Description, f => f.Random.AlphaNumeric(100))
                .RuleFor(u => u.Price, f => f.Random.Double())
                .RuleFor(u => u.StatusCode, f => f.Random.Enum<Status>())
                .Generate(count);
        }

        public static ProductWrite GetFakeProductWrite()
        {
            return GetFakeProductsWrite(1)[0];
        }

        public static List<ProductUpdate> GetFakeProductsUpdate(int count)
        {
            return new Faker<ProductUpdate>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Code, f => f.Commerce.Ean13())
                .RuleFor(u => u.Stock, f => f.Random.Number(1000))
                .RuleFor(u => u.Description, f => f.Random.AlphaNumeric(100))
                .RuleFor(u => u.Price, f => f.Random.Double())
                .RuleFor(u => u.StatusCode, f => f.Random.Enum<Status>())
                .Generate(count);
        }

        public static ProductUpdate GetFakeProductUpdate()
        {
            return GetFakeProductsUpdate(1)[0];
        }

        public static ProductUpdate GetFakeProductUpdate(Guid id)
        {
            var fakeModel = GetFakeProductsUpdate(1)[0];
            fakeModel.Id = id;

            return fakeModel;
        }

        public static List<ProductRead> GetFakeProductsRead(int count)
        {
            return new Faker<ProductRead>()
                .RuleFor(u => u.Name, f => f.Commerce.ProductName())
                .RuleFor(u => u.Code, f => f.Commerce.Ean13())
                .RuleFor(u => u.Stock, f => f.Random.Number(1000))
                .RuleFor(u => u.Description, f => f.Random.AlphaNumeric(100))
                .RuleFor(u => u.Price, f => f.Random.Double())
                .RuleFor(u => u.StatusName, f => f.Random.Enum<Status>().ToString())
                .Generate(count);
        }

        public static ProductRead GetFakeProductRead()
        {
            return GetFakeProductsRead(1)[0];
        }

        public static ProductQuery GetFakeProductQuery()
        {
            return new Faker<ProductQuery>()
                .RuleFor(u => u.Id, f => f.Random.Guid());
        }

        public static ProductQuery GetFakeProductQuery(Guid id)
        {
            return new ProductQuery() { Id = id };
        }
    }
}
