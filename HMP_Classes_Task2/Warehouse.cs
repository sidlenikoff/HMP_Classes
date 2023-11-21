using System.Text.Json.Serialization;

namespace HMP_Classes_Task2
{
    public class Warehouse
    {
        public int Capacity { get; set; }
        List<Courier> couriers;

        [JsonIgnore]
        List<Order>[] ordersInTimeMoment;

        [JsonIgnore]
        PriorityQueue<Baker, int> bakersQueue;

        public Warehouse(int capacity, List<Courier> couriers, int workdayLengthInSeconds)
        {
            Capacity = capacity;
            this.couriers = couriers;
            ordersInTimeMoment = new List<Order>[workdayLengthInSeconds];
            for (int i = 0; i < ordersInTimeMoment.Length; i++)
                ordersInTimeMoment[i] = new List<Order>();
            bakersQueue = new PriorityQueue<Baker, int>();
        } 

        public void ProcedueBaker(Baker baker)
        {
            if (ordersInTimeMoment[baker.Time].Count + 1 <= Capacity)
                for (int i = baker.Time; i < ordersInTimeMoment.Length; i++)
                {
                    if (ordersInTimeMoment[i].Count + 1 > Capacity)
                    {
                        var bakerToMove = ordersInTimeMoment[i][0].Baker;
                        if (!bakerToMove.IsInQueueToWarehouse)
                        {
                            bakersQueue.Enqueue(bakerToMove, bakerToMove.Time);
                            bakerToMove.IsInQueueToWarehouse = true;
                            ordersInTimeMoment[i].RemoveAt(0);
                        }
                    }
                    baker.CurrentOrder.SetInQueueToWarehouseTime(0);
                    ordersInTimeMoment[i].Add(baker.CurrentOrder);
                    ordersInTimeMoment[i] = ordersInTimeMoment[i].OrderByDescending(o => o.Baker.Time).ToList();
                }
            else
            {
                bakersQueue.Enqueue(baker, baker.Time);
                baker.IsInQueueToWarehouse = true;
            }
        }

        public void ProcedureOrdersInQueue()
        {
            for(int i = 0; i < ordersInTimeMoment.Length; i++)
            {
                var freeCouriers = couriers.Where(c => c.Time <= i).ToList();
                foreach(var courier in freeCouriers)
                {
                    if (ordersInTimeMoment[i].Count == 0)
                        break;
                    int ordersToTake = Math.Min(ordersInTimeMoment[i].Count, courier.GetMaxOrdersToDeliver());
                    ordersInTimeMoment[i] = ordersInTimeMoment[i].OrderBy(o => o.Baker.Time).ToList();
                    var ordersTaken = ordersInTimeMoment[i].GetRange(0, ordersToTake);
                    courier.TakeOrders(ordersTaken);
                    for (int j = courier.Time; j < ordersInTimeMoment.Length; j++)
                        foreach (var o in ordersTaken)
                            ordersInTimeMoment[j].Remove(o);
                    courier.DeliverOrders();
                }
            }
        }

        public void ProcedureBakersInQueue()
        {
            while (bakersQueue.Count > 0)
            {
                var firstBaker = bakersQueue.Peek();
                bool isAnyFreePlaces = false;
                for (int i = firstBaker.Time; i < ordersInTimeMoment.Length; i++)
                {
                    if (ordersInTimeMoment[i].Count + 1 <= Capacity)
                    {
                        firstBaker.CurrentOrder.SetInQueueToWarehouseTime(i - firstBaker.Time);
                        firstBaker.Time = i;
                        for(int j = i; j < ordersInTimeMoment.Length; j++)
                            ordersInTimeMoment[j].Add(firstBaker.CurrentOrder);
                        isAnyFreePlaces = true;
                        firstBaker.IsInQueueToWarehouse = false;
                        bakersQueue.Dequeue();
                        break;
                    }
                }
                if (!isAnyFreePlaces)
                    break;
            }
        }
    }
}
