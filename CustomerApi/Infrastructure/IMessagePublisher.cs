using CustomerApi.HiddenModel;

namespace CustomerApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void ResponseCustomerExists(HiddenCustomer customer);

    }
}
