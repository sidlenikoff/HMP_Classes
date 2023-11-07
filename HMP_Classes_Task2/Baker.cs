using System.Text.Json.Serialization;

namespace HMP_Classes_Task2
{
    public class Baker
    {
        public int ID { get; set; }

        [JsonIgnore]
        public Order CurrentOrder { get; set; }

        public int Performance { get; set; }

        [JsonIgnore]
        public int Time { get; set; }

        public Baker(int iD, int performance)
        {
            ID = iD;
            Performance = performance;
            Time = 0;
        }

        public void Cook(Order order)
        {
            CurrentOrder = order;
            order.Baker = this;
            Time += (int)Constants.TimeToCookOrder.TotalSeconds / Performance;
            order.SetInOvenTime((int)Constants.TimeToCookOrder.TotalSeconds / Performance);
        }

        public override string ToString()
        {
            return $"Пекарь {ID}";
        }

        public override int GetHashCode()
        {
            return $"ПЕКАРЬ{ID}".GetHashCode();
        }
    }
}