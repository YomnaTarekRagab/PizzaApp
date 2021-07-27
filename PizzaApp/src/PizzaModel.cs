using System.Collections.Generic;
namespace PizzaApp
{
    public record typeXPrice (string type, float price);
    public class PizzaModel{
        public List <typeXPrice> Toppings { get; set; }
        public List<typeXPrice> Sizes { get; set; }
        public  List<typeXPrice> Sides { get; set; }
    }
        
}