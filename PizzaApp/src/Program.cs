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
            //--Begin
            AnsiConsole.Render(
                new FigletText("PIZZA MENU")
                .Centered()
                .Color(Color.Yellow));
            AnsiConsole.Render(new Markup("[bold yellow] Enter the number of pizza orders:[/] \n"));
            Int32.TryParse(Console.ReadLine(),out int n); 
            //--Reading JSON from file
            string fileName = "PizzaMenu.json";
            PizzaModel pizzaMenu =DeserializeFile(fileName);
            //
            int i=0;
            float totalOrderPrice=0f;
            Pizza.pizzaMenu = pizzaMenu;
            Order prefOrder= new Order();
            while(i<n)
            {
                AnsiConsole.Render(new Markup($"[bold red]This is order number {i} from your {n} orders:[/] \n"));
                //
                typeXPrice prefTop =null ,prefSize = null,prefSide = null;
                ConsoleFn(pizzaMenu,ref prefTop,ref prefSize, ref prefSide);
                //--Create a pizza Object
                Pizza p1= new Pizza(prefTop,prefSize,prefSide);
                prefOrder.AddPizza(p1);
                i++;
                //
                if(i==n)
                   totalOrderPrice= prefOrder.OrderPrice();
                
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
            System.Console.WriteLine("The total order price is {0}" , totalOrderPrice);

            // Sets the user's preferences as a PizzaModel object and serializes it into JSON to be printed in JSON format 


            // var OrderTable= new Table();
            // OrderTable.AddColumn("[bold purple] UserId[/]");
            // OrderTable.AddColumn("[bold purple]Number of Pizzas[/]");
            // OrderTable.AddColumn("[bold purple]Total Order Price[/]");
            // OrderTable.AddRow($"{prefOrder.UserId}",$"{prefOrder.NumOfPizzas}", $"{prefOrder.TotalPrice}");
            // AnsiConsole.Render(OrderTable);


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

        static PizzaModel DeserializeFile(string fileName){
            string jsonString = File.ReadAllText(fileName);
            //--Deserialize the jsonString to print it on the console
            var pizzaMenu = JsonConvert.DeserializeObject<PizzaModel>(jsonString); 
            return pizzaMenu;
        }

        static void ConsoleFn(PizzaModel pizzaMenu , ref typeXPrice prefTop, ref typeXPrice prefSize, ref typeXPrice prefSide){
                //--Table formatting
                string formatTitle="[bold green]Available toppings[/] \n";
                List<String> columnNames =  new List<string> {"Toppings","Prices"};
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
                columnNames.Add("Prices");
                Menu.PrintMenu(formatTitle , columnNames , pizzaMenu.Sides);
                //
                AnsiConsole.Render(new Markup("[bold yellow]Your preferred side from the sides list:[/]\n"));
                //--Customer's Chosen Sides
                prefSide=Menu.InputCheck(pizzaMenu.Sides , "side");
        }
    }

}