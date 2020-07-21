using System;
using Grpc.Net.Client;
using gRPCDemo.Protos;
using gRPCDemo;
using System.Threading.Tasks;
using System.Linq;
using Grpc.Core;

namespace gRPCClient
{
    class Program
    {
        static  async Task Main(string[] args)
        {

            Console.WriteLine("============= Unary mode Example=====================");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var productsClient = new ProductGrpcService.ProductGrpcServiceClient(channel);
            var productResponse = await productsClient.GetProductsAsync(new ProductRequest());
            var products = productResponse.Products.ToList<Products>();
            foreach (var product in products)
            {
                Console.WriteLine($"Product Id: {product.ProductId},Name :  {product.Name},  Description : {product.ProductDescription},  Price: {product.ProductPrice}");
            }

            Console.WriteLine("\n\r\n\r");
            Console.WriteLine("============= Server Streaming Example=====================");

            using (var call = productsClient.GetNewProducts(new ProductRequest()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var product = call.ResponseStream.Current;

                    Console.WriteLine($"Product Id: {product.ProductId},Name :  {product.Name},  Description : {product.ProductDescription},  Price: {product.ProductPrice}");
                }
            }


            Console.ReadLine();
        }
    }
}
