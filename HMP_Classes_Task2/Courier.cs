using System.Text.Json.Serialization;

namespace HMP_Classes_Task2
{
    public class Courier
    {
        public int ID { get; set; }

        [JsonIgnore]
        public List<Order> CurrentOrders { get; set; }

        public int TrunkCapacity { get; set; }
        public int Performance { get; set; }

        [JsonIgnore]
        public int Time { get; set; }

        public Courier(int iD, int trunkCapacity, int performance)
        {
            ID = iD;
            CurrentOrders = new List<Order>();
            TrunkCapacity = trunkCapacity;
            Performance = performance;
            Time = 0;
        }

        public int GetMaxOrdersToDeliver() => TrunkCapacity - CurrentOrders.Count;

        public void TakeOrders(List<Order> orders)
        {
            CurrentOrders.AddRange(orders);
        }

        public void DeliverOrders()
        {
            int ordersCount = 0;
            while(CurrentOrders.Count > 0)
            {
                var order = CurrentOrders.First();
                order.Courier = this;
                Time += (int)Constants.TimeToDeliverOrder.TotalSeconds / Performance;
                order.SetInDeliveryTime(((int)Constants.TimeToDeliverOrder.TotalSeconds / Performance)*(ordersCount+1));
                order.Complete();
                ordersCount++;
                CurrentOrders.RemoveAt(0);
            }
        }

        public override string ToString()
        {
            return $"Курьер {ID}";
        }

        public override int GetHashCode()
        {
            return $"КУРЬЕР{ID}".GetHashCode();
        }
    }
}
