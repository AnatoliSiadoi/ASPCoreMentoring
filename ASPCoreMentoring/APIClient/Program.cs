using APIClient.Client.Interfaces;
using APIClient.IoC;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = CreateContainer();

                var categoryResource = container.Get<ICategoryResource>();
                var categoryList = categoryResource.GetCategoriesList().GetAwaiter().GetResult();
                WriteToConsole(categoryList, "Categories");

                var productResource = container.Get<IProductResource>();
                var productList = productResource.GetProductsList().GetAwaiter().GetResult();
                WriteToConsole(productList, "Products");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something went wrong!");
            }

            Console.ReadKey();
        }

        private static void WriteToConsole<T>(IEnumerable<T> list, string label)
        {
            string resultString = string.Empty;

            Console.WriteLine($"{label}:");
            Console.WriteLine(new string('-', 25));

            if(list.Any())
            {
                resultString = string.Join(Environment.NewLine, list.Select(i => JsonConvert.SerializeObject(i)));
            }
            else
            {
                resultString = "Empty list";
            }

            Console.WriteLine(resultString);
        }

        private static IKernel CreateContainer()
        {
            var container = new StandardKernel(new APIClientModule());
            return container;
        }
    }
}
