using System.Collections.Generic;
namespace PizzaApp
{
    public class PizzaModel{
    public List <string> Toppings { get; set; }
    public List<PSizes> Sizes { get; set; }
    public  List<string> Sides { get; set; }
    }
    public record PSizes(string Type, float Price);
        
}