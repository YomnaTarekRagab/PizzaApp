using System.Collections.Generic;
namespace PizzaApp
{
    public class PizzaModel{
    public List <typeXPrice> Toppings { get; set; }
    public List<typeXPrice> Sizes { get; set; }
    public  List<typeXPrice> Sides { get; set; }
    }
    public record typeXPrice(string Type, float Price);
        
}