using System.Collections.Generic;
namespace PizzaApp{
    public class Order{
    
        public const float TAXES = 15.0f;
        private static int currentId = 1;
        private float totalPrice;
        public int NumOfPizzas {get; set;}
        public int UserId {get;set;}
        public List<Pizza> ListOfPizzas {get;set;}
        public Order(){
            UserId = currentId;
            currentId++;
            ListOfPizzas = new List<Pizza> (NumOfPizzas);
        }
        public bool AddPizza(Pizza tobeAddedPizza){
            try{
                ListOfPizzas.Add(tobeAddedPizza);
                return true;
            }
            catch(KeyNotFoundException){
                return false;
            }
        }
        public float OrderPrice(){
            foreach(Pizza item in ListOfPizzas){
                totalPrice+= item.CalculatePrice();
            }
            totalPrice+= TAXES;
            return this.totalPrice;
        }
    }
}