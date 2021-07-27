using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;
using System.Text.Json;
namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //--Begin
            AnsiConsole.Render(
                new FigletText("PIZZA MENU")
                .Centered()
                .Color(Color.Yellow));
            bool progStart = true;
            //--Generic file path
            string fileName = "PizzaMenu.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            path = path.Split("PizzaApp\\")[0] + "PizzaApp\\";
            String ordersPath = path + "Orders.json";
            //--Delete previously created fle
            File.Delete(ordersPath);
            while (progStart)
            {
                bool currCustomer = true;
                while (currCustomer)
                {
                    AnsiConsole.Render(new Markup("[bold yellow] Welcome! Start placing your order below...[/]\n"));
                    AnsiConsole.Render(new Markup("[bold yellow] Enter the number of pizzas:[/] \n"));
                    Int32.TryParse(Console.ReadLine(), out int n);
                    //--Reading JSON from file
                    PizzaModel pizzaMenu = DeserializeFile(fileName);
                    int i = 0;
                    float totalOrderPrice = 0f;
                    Order prefOrder = new Order();
                    //--Setting num of pizzas
                    prefOrder.NumOfPizzas = n;
                    prefOrder.ListOfPizzas = new List<Pizza>();
                    while (i < n)
                    {
                        AnsiConsole.Render(new Markup($"[bold red]This is order number {i} from your {n} orders:[/] \n"));
                        TypeXPrice prefTop = null, prefSize = null, prefSide = null;
                        ConsoleFn(pizzaMenu, ref prefTop, ref prefSize, ref prefSide);
                        //--Create a pizza Object
                        Pizza p1 = new Pizza(prefTop, prefSize, prefSide);
                        prefOrder.ListOfPizzas.Add(p1);
                        i++;
                        if (i == n)
                            totalOrderPrice = prefOrder.OrderPrice();
                    }
                    //--Serialize JSON directly to a file
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var json = System.Text.Json.JsonSerializer.Serialize(prefOrder, options);
                    File.AppendAllText(ordersPath, json + Environment.NewLine);
                    //--Customer order finished
                    Console.WriteLine("Your order has been completed!");
                    System.Console.WriteLine("The total order price is {0}", totalOrderPrice);
                    //--End of loop
                    currCustomer = false;
                }
            }

            static PizzaModel DeserializeFile(string fileName)
            {
                string jsonString = File.ReadAllText(fileName);
                var pizzaMenu = JsonConvert.DeserializeObject<PizzaModel>(jsonString);
                return pizzaMenu;
            }

            static void ConsoleFn(PizzaModel pizzaMenu, ref TypeXPrice prefTop, ref TypeXPrice prefSize, ref TypeXPrice prefSide)
            {
                //--Table formatting
                string formatTitle = "[bold green]Available toppings[/] \n";
                List<String> columnNames = new List<string> { "Toppings", "Prices" };
                Menu.PrintMenu(formatTitle, columnNames, pizzaMenu.Toppings);
                AnsiConsole.Render(new Markup("[bold yellow] Your preferred topping from the topping list:[/] \n"));
                //--Customer's Toppings
                prefTop = Menu.InputCheck(pizzaMenu.Toppings, "topping");
                //--Table formatting
                formatTitle = "[bold green]Available sizes[/] \n";
                columnNames.Clear();
                columnNames.Add("Sizes");
                columnNames.Add("Prices");
                Menu.PrintMenu(formatTitle, columnNames, pizzaMenu.Sizes);
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred pizza size from the sizes list:[/]\n"));
                //--Customer's Chosen Size
                prefSize = Menu.InputCheck(pizzaMenu.Sizes);
                //--Table formatting
                formatTitle = "[bold green]Available sides[/] \n";
                columnNames.Clear();
                columnNames.Add("Sides");
                columnNames.Add("Prices");
                Menu.PrintMenu(formatTitle, columnNames, pizzaMenu.Sides);
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred side from the sides list:[/]\n"));
                //--Customer's Chosen Sides
                prefSide = Menu.InputCheck(pizzaMenu.Sides, "side");
            }
        }
    }
}