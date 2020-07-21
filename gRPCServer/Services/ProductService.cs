using Grpc.Core;
using gRPCDemo.Protos;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPCDemo.Services
{
    public class ProductService : ProductGrpcService.ProductGrpcServiceBase
    {
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        List<Products> newProducts = new List<Products>()
            {
                new Products
                {
                    Name = "New Chair",
                    ProductDescription = "New Awesome Chair",
                    ProductId = 1001,
                    ProductPrice = 50.00F
                },
                new Products
                {
                    Name = "New Table",
                    ProductDescription = "New Awesome Table",
                    ProductId = 1002,
                    ProductPrice = 150.00F
                },
                new Products
                {
                    Name = "New Sofa",
                    ProductDescription = "New Awesome Sofa",
                    ProductId = 1003,
                    ProductPrice = 650.00F
                }
            };

        public override Task<ProductResponse> GetProducts(ProductRequest request, ServerCallContext context)
        {
            ProductResponse response = new ProductResponse();
            response.Products.AddRange(newProducts);
            return Task.FromResult(response);
        }

        public override async Task GetNewProducts(ProductRequest request, IServerStreamWriter<Products> responseStream, ServerCallContext context)
        {
            foreach (var product in newProducts)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(product);
            }
        }
    }
}
