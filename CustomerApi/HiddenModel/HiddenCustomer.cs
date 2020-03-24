namespace CustomerApi.HiddenModel
{
    public class HiddenCustomer
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public float CreditStanding { get; set; }
    }
}
