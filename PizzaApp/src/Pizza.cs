using System.Collections.Generic;
namespace PizzaApp
{
    public class Pizza{
        private float price = 0;
        public typeXPrice Topping { get; set; }
        public typeXPrice Size { get; set; }
        public typeXPrice Side { get; set; }
        public Pizza(typeXPrice top, typeXPrice size, typeXPrice side){
            this.Topping = top;
            this.Size = size;
            this.Side = side;
        }
        public float CalculatePrice(){
            if(this.price==0)
                this.price+=this.Topping.price+this.Size.price+this.Side.price;
            return this.price;
        }

    }
        
}