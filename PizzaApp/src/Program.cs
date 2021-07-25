using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;

namespace PizzaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Render(
            new FigletText("PIZZA MENU")
            .Centered()
            .Color(Color.Yellow));
            string fileName = "PizzaMenu.json";
            string jsonString = File.ReadAllText(fileName);
            //--Deserialize the jsonString to print it on the console
            var pizzaMenu = JsonConvert.DeserializeObject<PizzaModel>(jsonString); 
            //
            AnsiConsole.Render(new Markup("[bold yellow] Enter the number of pizza orders:[/] \n"));
            Int32.TryParse(Console.ReadLine(),out int n); 
            int i=0;
            Random rnd = new Random();
            int RandId  = rnd.Next(1000);  
            float SumOfPrices=0;
            string prefTop = "";
            string prefSize = "";
            string prefSide = "";
            var prefOrder= new Order();
            while(i<n)
            {
                //--Table formatting
                string formatTitle="[bold green]Available toppings[/] \n";
                List<String> columnNames =  new List<string> {"Toppings"};
                Menu.PrintMenu(formatTitle,columnNames,pizzaMenu.Toppings);
                //
                AnsiConsole.Render(new Markup("[bold yellow] Your preferred topping from the topping list:[/] \n"));
                //--Customer's Toppings
                prefTop= Menu.InputCheck(pizzaMenu.Toppings,"topping");
                //--Table formatting
                formatTitle = "[bold green]Available sizes[/] \n";
                columnNames.Clear();
                columnNames.Add("Sizes");
                columnNames.Add("Prices");
                Menu.PrintMenu(formatTitle , columnNames , pizzaMenu.Sizes);
                //
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred pizza size from the sizes list:[/]\n"));
                //--Customer's Chosen Size
                prefSize=Menu.InputCheck(pizzaMenu.Sizes);
                //--Table formatting
                formatTitle="[bold green]Available sides[/] \n";
                columnNames.Clear();
                columnNames.Add("Sides");
                Menu.PrintMenu(formatTitle , columnNames , pizzaMenu.Sides);
                //
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred side from the sides list:[/]\n"));
                //--Customer's Chosen Sides
                prefSide=Menu.InputCheck(pizzaMenu.Sides , "side");


                foreach (var item in pizzaMenu.Sizes)
                {
                    if(string.Equals(prefSize, item.Type, StringComparison.OrdinalIgnoreCase))
                    {
                        SumOfPrices+=item.Price;
                        AnsiConsole.Render(new Markup($"[bold blue]Price for this order is: {item.Price}[/]\n"));
                    }
                }
                prefOrder.ChosenTop=prefTop;
                prefOrder.ChosenSize=prefSize;
                prefOrder.ChosenSide=prefSide;
                prefOrder.UserId=RandId;
                prefOrder.NumOfPizzas=n;
                prefOrder.TotalPrice=SumOfPrices;
                i++;
                //--Generic path to Pizza App project
                string path = Path.Combine(Environment.CurrentDirectory, @"Data\", fileName);
                path = path.Split("PizzaApp\\")[0]+"PizzaApp\\";
                String ordersPath = path+"Orders.json";
                File.WriteAllText(ordersPath, JsonConvert.SerializeObject(prefOrder));

                // serialize JSON directly to a file
                using (StreamWriter file = File.CreateText(ordersPath))
                {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, prefOrder);
                }

            }
            Console.WriteLine("Your order has been completed!");

            // Sets the user's preferences as a PizzaModel object and serializes it into JSON to be printed in JSON format 


            var OrderTable= new Table();
            OrderTable.AddColumn("[bold purple] UserId[/]");
            OrderTable.AddColumn("[bold purple]Number of Pizzas[/]");
            OrderTable.AddColumn("[bold purple]Total Order Price[/]");
            OrderTable.AddRow($"{prefOrder.UserId}",$"{prefOrder.NumOfPizzas}", $"{prefOrder.TotalPrice}");
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