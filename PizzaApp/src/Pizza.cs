using System.Collections.Generic;
namespace PizzaApp
{
    public class Pizza{
        public typeXPrice topping;
        public typeXPrice size;
        public typeXPrice side;
        private float price=0;
        public static PizzaModel pizzaMenu;
        //--Methods
        public Pizza(typeXPrice top, typeXPrice size, typeXPrice side){
            this.topping =top;
            this.size=size;
            this.side=side;
        }
        //
        public float CalculatePrice(){
            if(this.price==0)
                this.price+=this.topping.Price+this.size.Price+this.side.Price;
            return this.price;
        }

    }
        
}