using System;
using System.Text.Json;

namespace HMP_Classes_Task2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Baker> bakers = new List<Baker>() { new Baker(0, 3), new Baker(1, 3), new Baker(2, 3), new Baker(3, 3), new Baker(4, 3) };

            List<Courier> couriers = new List<Courier>() { new Courier(1,2,3), new Courier(1, 2, 3) , 
                new Courier(1, 2, 3) , 
                new Courier(1, 2, 3) ,};

            List<Order> orders = new List<Order>() { new Order(0, TimeSpan.FromMinutes(120)), new Order(1, TimeSpan.FromMinutes(120)),
            new Order(2, TimeSpan.FromMinutes(120)), new Order(3, TimeSpan.FromMinutes(120)), new Order(4, TimeSpan.FromMinutes(120)),
            new Order(5, TimeSpan.FromMinutes(120)), new Order(6, TimeSpan.FromMinutes(120)), new Order(7, TimeSpan.FromMinutes(120))};
            
            Pizzeria pizzeria = new Pizzeria(orders, bakers, couriers, 2);

            pizzeria.ProcedureOrders();
            pizzeria.GetReport();

            /*using (FileStream fs = new FileStream("pizzeria.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<Pizzeria>(fs, pizzeria);
                Console.WriteLine("Data has been saved to file");
            }*/

            /*using (FileStream fs = new FileStream("pizzeria.json", FileMode.OpenOrCreate))
            {
                Pizzeria? pizzeria = JsonSerializer.Deserialize<Pizzeria>(fs);
                if (pizzeria != null)
                {
                    pizzeria.ProcedureOrders();
                    pizzeria.GetReport();
                }
            }*/
        }
    }
}