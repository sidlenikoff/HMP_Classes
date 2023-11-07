namespace HMP_Classes_Task2
{
    public class Pizzeria
    {
        public List<Order> Orders { get; set; }
        public List<Baker> Bakers { get; set; }
        public List<Courier> Couriers { get; set; }
        public int WarehouseCapacity { get; set; }

        Warehouse warehouse;
        List<Order> complitedOrders;

        public Pizzeria(List<Order> orders, List<Baker> bakers, List<Courier> couriers, int warehouseCapacity)
        {
            this.Orders = orders;
            this.Bakers = bakers;
            this.Couriers = couriers;
            WarehouseCapacity = warehouseCapacity;
            warehouse = new Warehouse(warehouseCapacity, this.Couriers, (int)Constants.WorkdayLength.TotalSeconds);
            complitedOrders = orders.Where(o => o.Status == Constants.OrderStatuses.Complited).ToList();
            foreach (var order in complitedOrders)
                orders.Remove(order);
        }

        public void ProcedureOrders()
        {
            while (Orders.Count > 0)
            {
                int orderIndex = 0;
                foreach (var baker in Bakers)
                {
                    if (orderIndex >= Orders.Count)
                        break;

                    var order = Orders[orderIndex++];
                    order.SetInQueueTime(baker.Time);
                    baker.Cook(order);
                    if (baker.Time >= (int)Constants.WorkdayLength.TotalSeconds)
                        break;
                    warehouse.ProcedueBaker(baker);
                }

                warehouse.ProcedureOrdersInQueue();
                warehouse.ProcedureBakersInQueue();

                var completed = Orders.Where(o => o.Status == Constants.OrderStatuses.Complited);
                complitedOrders.AddRange(completed);
                foreach (var c in complitedOrders)
                    Orders.Remove(c);
            }
        }

        public void GetReport()
        {
            var freeOrders = complitedOrders.Where(o => o.GetActualComplitionTime() > o.ExpectedComplitionTime);
            if (freeOrders.Count() == 0)
            {
                var lastOrderComplited = complitedOrders.Max(o => o.GetActualComplitionTime());
                if (Constants.TimeToCookOrder + Constants.TimeToDeliverOrder + lastOrderComplited <= Constants.WorkdayLength) 
                    Console.WriteLine("УВЕЛИЧИТЬ ЧИСЛО ЗАКАЗОВ");
                return;
            }
            Dictionary<Baker, int> bakersWithPoints = new Dictionary<Baker, int>();
            Dictionary<Courier, int> couriersWithPoints = new Dictionary<Courier, int>();
            int InQueueToWarehousePoints = 0;
            int InQueuePoints = 0;
            foreach (var order in freeOrders)
            {
                Constants.OrderStatuses slowestStage = order.GetStageWithMaxTime();
                switch (slowestStage)
                {
                    case Constants.OrderStatuses.InQueue:
                        InQueuePoints++;
                        break;
                    case Constants.OrderStatuses.InOven:
                        if (bakersWithPoints.ContainsKey(order.Baker))
                            bakersWithPoints[order.Baker]++;
                        else
                            bakersWithPoints.Add(order.Baker, 1);
                        break;
                    case Constants.OrderStatuses.InQueueToWarehouse:
                        InQueueToWarehousePoints++;
                        break;
                    case Constants.OrderStatuses.InDelivery:
                        if (couriersWithPoints.ContainsKey(order.Courier))
                            couriersWithPoints[order.Courier]++;
                        else
                            couriersWithPoints.Add(order.Courier, 1);
                        break;
                }
            }

            var slowestBaker = bakersWithPoints.Where(b => b.Value == bakersWithPoints.Max(b => b.Value));
            var slowestCourier = couriersWithPoints.Where(b => b.Value == couriersWithPoints.Max(b => b.Value));
            if (InQueuePoints > freeOrders.Count() * 0.2 || slowestBaker.Count() == Bakers.Count)
                Console.WriteLine("НАНЯТЬ НОВОГО ПЕКАРЯ");
            if(slowestBaker.Count() != Bakers.Count)
                foreach (var b in slowestBaker)
                    Console.WriteLine($"УВОЛИТЬ ПЕКАРЯ {b.Key}");

            if (InQueueToWarehousePoints > freeOrders.Count() * 0.2 || slowestCourier.Count() == Couriers.Count)
                Console.WriteLine("РАСШИРИТЬ СКЛАД ИЛИ НАНЯТЬ БОЛЬШЕ КУРЬЕРОВ");
            if(slowestCourier.Count() != Couriers.Count)
                foreach (var c in slowestCourier)
                    Console.WriteLine($"УВОЛИТЬ КУРЬЕРА {c.Key}");


        }
    }
}
