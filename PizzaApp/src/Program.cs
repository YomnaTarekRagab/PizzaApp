using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;

namespace PizzaApp
{
public class PizzaModel
    {
        public string[] Toppings { get; set; }
        public PSizes[] Sizes { get; set; }
        public string[] Sides { get; set; }
    }
    public record PSizes(string Type, int Price);

    public class Order
    {
        public int UserId { get; set; }
        public string ChosenTop { get; set; }
        public string ChosenSize { get; set; }
        public string ChosenSide { get; set; }
        public int NumOfPizzas { get; set; }
        public int TotalPrice { get; set; }
        public Order()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "PizzaMenu.json";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
            path = path.Split("PizzaApp\\")[0] + "PizzaApp\\";
            string ordersPath = path + "Orders.json";
            File.Delete(ordersPath);
            AnsiConsole.Render(
            new FigletText("PIZZA MENU")
            .Centered()
            .Color(Color.Yellow));
            string jsonString = File.ReadAllText(fileName);
            // Deserialize the jsonString to print it on the console

            var pizzaMenu = JsonConvert.DeserializeObject<PizzaModel>(jsonString);
            AnsiConsole.Render(new Markup("[bold yellow] Enter the number of pizza orders:[/] \n"));
            int n = Convert.ToInt32(Console.ReadLine());
            int i = 0;
            Random rnd = new Random();
            int RandId = rnd.Next(1000);
            int SumOfPrices = 0;
            string prefTop = "";
            string prefSize = "";
            string prefSide = "";
            var prefOrder = new Order();
            //bool morePizza = true;
            while (i<n)
            {
                // Allow the user to place his order and checks if the entered option is from the available options or not
                // Doesn't proceed if the user entered nothing or entered an option not in the lists
                AnsiConsole.Render(new Markup("[bold green]Available toppings[/] \n"));
                var table = new Table();
                table.AddColumn(new TableColumn("Toppings").Centered());
                foreach (
                string item in pizzaMenu.Toppings)
                {

                    table.AddRow($"[bold Red]{item}[/]");

                }
                AnsiConsole.Render(table);
                AnsiConsole.Render(new Markup("[bold yellow] Your preferred topping from the topping list:[/] \n"));
                bool checkTop = false;

                while (!checkTop)
                {
                    prefTop = Console.ReadLine();
                    foreach (string item in pizzaMenu.Toppings)
                    {
                        if (prefTop == item)
                        {
                            AnsiConsole.Render(new Markup($"[bold blue] Chosen Topping is: {prefTop}[/]\n"));
                            checkTop = true;
                        }

                    }
                }
                AnsiConsole.Render(new Markup("[bold green]Available sizes[/] \n"));
                var table1 = new Table();
                table1.AddColumn(new TableColumn("Sizes").Centered());
                table1.AddColumn(new TableColumn("Prices").Centered());
                foreach (
                var item in pizzaMenu.Sizes)
                {
                    table1.AddRow(new Markup($"[bold Red]{item.Type}[/]"), new Markup($"[bold Red] ${item.Price}[/]"));
                }
                AnsiConsole.Render(table1);
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred pizza size from the sizes list:[/]\n"));
                bool checkSize = false;

                while (!checkSize)
                {
                    prefSize = Console.ReadLine();
                    foreach (var item in pizzaMenu.Sizes)
                    {
                        if (prefSize == item.Type)
                        {
                            AnsiConsole.Render(new Markup($"[bold blue]Chosen Size is: {prefSize}[/]\n"));
                            checkSize = true;
                        }

                    }
                }
                AnsiConsole.Render(new Markup("[bold green]Available sides[/] \n"));
                var table2 = new Table();
                table2.AddColumn(new TableColumn("Sides").Centered());
                foreach (
                string item in pizzaMenu.Sides)
                {
                    table2.AddRow($"[bold Red]{item}[/]");

                }
                AnsiConsole.Render(table2);
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred side from the sides list:[/]\n"));
                bool checkSide = false;

                while (!checkSide)
                {
                    prefSide = Console.ReadLine();
                    foreach (string item in pizzaMenu.Sides)
                    {
                        if (prefSide == item)
                        {
                            AnsiConsole.Render(new Markup($"[bold blue]Chosen Side is: {prefSide}[/]\n"));
                            checkSide = true;
                        }

                    }
                }

                foreach (var item in pizzaMenu.Sizes)
                {
                    if (prefSize == item.Type)
                    {
                        SumOfPrices += item.Price;
                        AnsiConsole.Render(new Markup($"[bold blue]Price for this order is: {item.Price}[/]\n"));
                        checkSize = true;
                    }
                }
               
                prefOrder.ChosenTop = prefTop;
                prefOrder.ChosenSize = prefSize;
                prefOrder.ChosenSide = prefSide;
                prefOrder.UserId = RandId;
                prefOrder.NumOfPizzas = n;
                prefOrder.TotalPrice = SumOfPrices;
                i++;
                File.AppendAllText(ordersPath, JsonConvert.SerializeObject(prefOrder) + Environment.NewLine);
                // serialize JSON directly to a file
                //  using (StreamWriter file = new(ordersPath, append: true))
                //  {
                //  JsonSerializer serializer = new JsonSerializer();
                //  serializer.Serialize(file, prefOrder);
                //  }
                // var answer = AnsiConsole.Prompt(
                //     new SelectionPrompt<string>()
                //         .Title("Do you want to order another [green] pizza[/]?")
                //         .AddChoices(new[] { "Yes", "No" })
                //         );
                // if (answer == "Yes")
                // {
                //     morePizza = true;

                // }
                // else
                // {
                //     morePizza = false;
                //     AnsiConsole.Render(new Markup($"[bold blue]Come Back again![/]\n"));


                // }
            }
            // Console.WriteLine("Your order has been completed!");
            // Sets the user's preferences as a PizzaModel object and serializes it into JSON to be printed in JSON format 


            var OrderTable = new Table();
            OrderTable.AddColumn("[bold purple] UserId[/]");
            OrderTable.AddColumn("[bold purple]Number of Pizzas[/]");
            OrderTable.AddColumn("[bold purple]Total Order Price[/]");
            OrderTable.AddRow($"{prefOrder.UserId}", $"{prefOrder.NumOfPizzas}", $"{prefOrder.TotalPrice}");
            AnsiConsole.Render(OrderTable);


            // string jsonOrder = JsonSerializer.Serialize(prefOrder);
            // string jsonOrder2 = JsonSerializer.Serialize(orderArray);
            // Console.WriteLine(jsonOrder);
            // Console.WriteLine(jsonOrder2);
            // Console.WriteLine("Do you want to order another pizza? (yes/no)");
            // string answer= Console.ReadLine();
            // if(answer=="yes")
            // {
            //     morePizza=true;
            // }
            // else {
            //     morePizza=false;
            //     Console.WriteLine("Come back again!");
            // }

        }
    }
}