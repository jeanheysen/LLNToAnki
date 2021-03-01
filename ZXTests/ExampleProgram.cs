//using Newtonsoft.Json;
//using System;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace ZXTests
//{

//    public class Product
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }
//        public decimal Price { get; set; }
//        public string Category { get; set; }
//    }

//    class ExampleProgram
//    {
//        static HttpClient client = new HttpClient();

//        static void ShowProduct(Product product)
//        {
//            Console.WriteLine($"Name: {product.Name}\tPrice: " +
//                $"{product.Price}\tCategory: {product.Category}");
//        }

//        static async Task<Uri> CreateProductAsync(Product product)
//        {
//            var json = JsonConvert.SerializeObject(product);

//            var data = new StringContent(json, Encoding.UTF8, "application/json");

//            HttpResponseMessage response = await client.PostAsync("api/products", data);

//            response.EnsureSuccessStatusCode();

//            // return URI of the created resource.
//            return response.Headers.Location;
//        }



//        public static void Main()
//        {
//            RunAsync().GetAwaiter().GetResult();
//        }

//        static async Task RunAsync()
//        {
//            // Update port # in the following line.
//            client.BaseAddress = new Uri("http://localhost:64195/");
//            client.DefaultRequestHeaders.Accept.Clear();
//            client.DefaultRequestHeaders.Accept.Add(
//                new MediaTypeWithQualityHeaderValue("application/json"));

//            try
//            {
//                // Create a new product
//                Product product = new Product
//                {
//                    Name = "Gizmo",
//                    Price = 100,
//                    Category = "Widgets"
//                };

//                var url = await CreateProductAsync(product);
//                Console.WriteLine($"Created at {url}");

//                // Get the product
//                product = await GetProductAsync(url.PathAndQuery);
//                ShowProduct(product);

//                // Update the product
//                Console.WriteLine("Updating price...");
//                product.Price = 80;
//                await UpdateProductAsync(product);

//                // Get the updated product
//                product = await GetProductAsync(url.PathAndQuery);
//                ShowProduct(product);

//                // Delete the product
//                var statusCode = await DeleteProductAsync(product.Id);
//                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            Console.ReadLine();
//        }
//    }
//}
