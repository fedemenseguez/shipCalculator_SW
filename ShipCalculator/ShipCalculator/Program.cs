using Calculator_BLL.API;
using Calculator_BLL.Repository;
using System;
using Calculator_BLL.Helpers;

namespace ShipCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(StringHelper.input_console_string);
                int distance;

                while (!int.TryParse(Console.ReadLine(), out distance))
                {
                    Console.WriteLine(StringHelper.incorrectInputValue);
                    Console.WriteLine(StringHelper.input_console_string);
                }

                Console.WriteLine(StringHelper.gettingData);
                var api = new ExternalApi(new System.Net.Http.HttpClient());
                var ships = api.GetAllShips();
                var calculator = new Calculator();

                foreach (var item in ships)
                {
                    var countStop = calculator.CalculateStops(item.MGLT, item.consumables, distance);

                    Console.WriteLine(item.name + ": " + countStop);

                }
                Console.Read();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Something went wrong : " + Environment.NewLine  + " -      Internet might not be available." + Environment.NewLine + " -      The WEB API (" + StringHelper.urlbase+") might be down.");

                Console.Read();
            }

        }

    }
}
