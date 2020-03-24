using OrderApi.Models;

namespace OrderApi.Infrastructure
{
    public interface IMessagePublisher
    {
        void PublishCustomerExists(HiddenOrder order);

    }
}
