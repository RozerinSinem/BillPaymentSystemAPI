namespace BillPaymentSystem.Models
{
    public class DataModel
    {
        public class Subscriber
        {
            public int Id { get; set; }
            public string SubscriberNo { get; set; }
            public ICollection<Bill> Bills { get; set; }

        }
        public class Bill
        {
            public int Id { get; set; }
            public int SubscriberId { get; set; }
            public DateTime Month { get; set; }
            public decimal TotalAmount { get; set; }
            public bool IsPaid { get; set; }
            public Subscriber Subscriber { get; set; }
        }
    }
}
