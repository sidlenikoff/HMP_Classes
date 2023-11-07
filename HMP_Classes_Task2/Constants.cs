namespace HMP_Classes_Task2
{
    public static class Constants
    {
        public static readonly TimeSpan WorkdayLength = TimeSpan.FromSeconds(86400);
        public static readonly TimeSpan TimeToDeliverOrder = TimeSpan.FromMinutes(60);
        public static readonly TimeSpan TimeToCookOrder = TimeSpan.FromMinutes(60);
        public enum OrderStatuses { InQueue, InOven, InQueueToWarehouse, InDelivery, Complited }
    }
}
