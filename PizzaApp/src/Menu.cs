using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Spectre.Console;
namespace PizzaApp
{
    public class Menu{

        public static void PrintMenu<T>(string formatTitle, List<string> columnNames, List<T> items){
            //--Title
            AnsiConsole.Render(new Markup(formatTitle));
            var table = new Table();
            //--Columns
            foreach (string columnName in columnNames)
                table.AddColumn(new TableColumn(columnName).Centered());
            //--Rows
            foreach (var item in items){
                if(typeof(T) == typeof(PSizes)){
                    PSizes obj = (PSizes) (object) item;
                    table.AddRow(new Markup($"[bold Red]{obj.Type}[/]"), new Markup($"[bold Red] ${obj.Price}[/]"));
                } 
                else
                    table.AddRow($"[bold Red]{item}[/]"); 
            }
            AnsiConsole.Render(table);
        }

        public static string InputCheck <T>(List<T> inputArray, string type=""){
            bool check=false;
            string chosen="";
            while(!check){
                chosen=Console.ReadLine();
                foreach (var item in inputArray) {
                    //--Toppings And Sides Check
                    if(typeof(T)==typeof(string)){
                        string item2 = (string)(object) item;
                        if(string.Equals(chosen, item2, StringComparison.OrdinalIgnoreCase)){
                            if(type=="topping")
                                AnsiConsole.Render(new Markup($"[bold blue] Chosen Topping is: {chosen}[/]\n"));
                            else if(type=="side")
                                AnsiConsole.Render(new Markup($"[bold blue]Chosen Side is: {chosen}[/]\n"));
                            //
                            check =true;
                        }
                    }
                    //--Size Check
                    else if (typeof(T) == typeof(PSizes)){
                        PSizes obj = (PSizes) (object) item;
                        if(string.Equals(chosen, obj.Type, StringComparison.OrdinalIgnoreCase)){
                            AnsiConsole.Render(new Markup($"[bold blue]Chosen Size is: {chosen}[/]\n"));
                            check =true;
                        }
                    }

                }
            }
            return chosen;
        }
    }   
}