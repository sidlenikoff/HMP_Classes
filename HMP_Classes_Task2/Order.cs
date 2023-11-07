using System.Text.Json.Serialization;

namespace HMP_Classes_Task2
{
    public class Order
    {
        
        public int ID { get; set; }

        [JsonIgnore]
        public Baker Baker { get; set; }

        [JsonIgnore]
        public Courier Courier { get; set; }

        public TimeSpan ExpectedComplitionTime { get; set; }

        public Constants.OrderStatuses Status { get; set; }

        Dictionary<Constants.OrderStatuses, int> TimeOnStage;

        public Order(int iD, TimeSpan expectedComplitionTime)
        {
            ID = iD;
            ExpectedComplitionTime = expectedComplitionTime;
            Status = Constants.OrderStatuses.InQueue;
            TimeOnStage = new Dictionary<Constants.OrderStatuses, int>();
        }

        public void SetInOvenTime(int time)
        {
            SetTimeOnStage(Constants.OrderStatuses.InOven, time);
        }
        public void SetInQueueTime(int time)
        {
            SetTimeOnStage(Constants.OrderStatuses.InQueue, time);
        }
        public void SetInQueueToWarehouseTime(int time)
        {
            SetTimeOnStage(Constants.OrderStatuses.InQueueToWarehouse, time);
        }
        public void SetInDeliveryTime(int time)
        {
            SetTimeOnStage(Constants.OrderStatuses.InDelivery, time);
        }

        public void Complete()
        {
            Status = Constants.OrderStatuses.Complited;
            int sumTime = TimeOnStage.Sum(a => a.Value);
            SetTimeOnStage(Status, sumTime);
        }

        public TimeSpan GetActualComplitionTime() => TimeSpan.FromSeconds(TimeOnStage[Constants.OrderStatuses.Complited]);
        public Constants.OrderStatuses GetStageWithMaxTime()
        {
            int maxTime = int.MinValue;
            Constants.OrderStatuses status = Constants.OrderStatuses.Complited;
            foreach(var t in TimeOnStage)
            {
                if (t.Key == Constants.OrderStatuses.Complited)
                    continue;
                if(t.Value > maxTime)
                {
                    maxTime = t.Value;
                    status = t.Key;
                }    
            }

            return status;
        }


        private void SetTimeOnStage(Constants.OrderStatuses status, int time)
        {
            //
            if (TimeOnStage == null)
                TimeOnStage = new Dictionary<Constants.OrderStatuses, int>();
            if (!TimeOnStage.ContainsKey(status))
            {
                Console.WriteLine($"[{this.ID}] [{status}] [{TimeSpan.FromSeconds(time)}]");
                TimeOnStage.Add(status, time);
            }
            else
                TimeOnStage[status] = time;
        }

        public override string ToString()
        {
            return $"Заказ {ID}";
        }

        public override int GetHashCode()
        {
            return $"ЗАКАЗ{ID}".GetHashCode();
        }

    }
}
