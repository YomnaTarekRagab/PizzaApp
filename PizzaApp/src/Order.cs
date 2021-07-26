using System.Collections.Generic;
namespace PizzaApp{
    public class Order{
    
        public const float TAXES  =15.0f;
        private static int currentId=1;
        public int NumOfPizzas{get; set;}
        public int userId;
        public List<Pizza> listOfPizzas;
        public float totalPrice;
        //--Methods
        public Order(){
            userId = currentId;
            currentId++;
            listOfPizzas= new List<Pizza> (NumOfPizzas);
        }
        //
        public int GetUserID(){
            return userId;
        }
        //
        public bool AddPizza(Pizza tobeAddedPizza){
            try{
                listOfPizzas.Add(tobeAddedPizza);
                return true;
            }
            catch(KeyNotFoundException){
                return false;
            }
        }
        //
        public float OrderPrice(){
            foreach(Pizza item in listOfPizzas){
                totalPrice+= item.CalculatePrice();
            }
            totalPrice+=TAXES;
            return this.totalPrice;
        }
    }
}