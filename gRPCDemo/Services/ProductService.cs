using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using gRPCDemo.Protos;
using Microsoft.Extensions.Logging;

namespace gRPCDemo.Services
{
    public class ProductService : gRPCDemo.Protos.ProductGrpcService.ProductGrpcServiceBase
    {
        private readonly ILogger<ProductService> _logger;
        public ProductService(ILogger<ProductService> logger)
        {
            _logger = logger;
        }

        public override Task<ProductResponse> GetProducts(ProductRequest request, ServerCallContext context)
        {
            ProductResponse response = new ProductResponse();
            response.Products.Add(new Products { Name = "Awesome Chair", ProductDescription = "Simple Chair", ProductId = 1, ProductPrice = 100.00F });
            response.Products.Add(new Products { Name = "Awesome Table", ProductDescription = "Simple Table", ProductId = 2, ProductPrice = 300.00F });
            response.Products.Add(new Products { Name = "Awesome Sofa", ProductDescription = "Simple Sofa", ProductId = 3, ProductPrice = 400.00F });

            return Task.FromResult(response);
        }


        public override async Task GetNewProducts(ProductRequest request, IServerStreamWriter<Products> responseStream, ServerCallContext context)
        {
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
                    Name = "New Chair",
                    ProductDescription = "New Awesome Chair",
                    ProductId = 1001,
                    ProductPrice = 50.00F
                },
                new Products
                {
                    Name = "New Chair",
                    ProductDescription = "New Awesome Chair",
                    ProductId = 1001,
                    ProductPrice = 50.00F
                }
            };


            foreach (var product in newProducts)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(product);
            }

        }
    }
}
