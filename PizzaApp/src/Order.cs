using System.Collections.Generic;
namespace PizzaApp
{
    public class Order
    {
        public const float Taxes = 15.0f;
        private static int CurrentId = 1;
        private float TotalPrice;
        public int NumOfPizzas { get; set; }
        public int UserId { get; set; }
        public List<Pizza> ListOfPizzas { get; set; }

        public Order()
        {
            UserId = CurrentId;
            CurrentId++;
            ListOfPizzas = new List<Pizza>(NumOfPizzas);
        }

        public bool AddPizza(Pizza tobeAddedPizza)
        {
            try
            {
                ListOfPizzas.Add(tobeAddedPizza);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public float OrderPrice()
        {
            foreach (Pizza item in ListOfPizzas)
            {
                TotalPrice += item.CalculatePrice();
            }
            TotalPrice += Taxes;
            return this.TotalPrice;
        }
    }
}