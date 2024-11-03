using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaySpace.Calculation.Assessment.Console.Extension;
using PaySpace.Calculation.Assessment.Console.Service;
using Microsoft.Extensions.Logging;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Information);
        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddServices(context.Configuration);
    })
    .Build();

var calculateTaxService = host.Services.GetRequiredService<ICalculateTaxService>();
Console.WriteLine("===================Welcome to Tax Calculator===================");
while (true)
{
    Console.Write("Please enter your country full name: ");
    var countryName = Console.ReadLine();
    Console.Write("Do you want to update all income and calculated tax of your country?(y/n) ");
    var isUpadteTax = Console.ReadLine();
    var income = "0";
    if (isUpadteTax == "n")
    {
        while (true)
        {
            Console.Write("Please enter your income: ");
            income = Console.ReadLine();
            if (!double.TryParse(income, out double result))
            {
                Console.WriteLine("Invalid income input.");
            }
            else { break; }
        }
    }
    var response = await calculateTaxService.UpdateCalculatedTax(countryName, decimal.Parse(income)).ConfigureAwait(false);
    Console.WriteLine(response.Message);
    Console.Write("Would you like to perform another calculation? (y/n): ");
    var repeat = Console.ReadLine()?.ToLower();
    if (repeat != "y")
    {
        Console.WriteLine("Thank you for using the Tax Calculator. Goodbye!");
        break;
    }
}
host.Run();

