using SharedModels;

namespace OrderApi.Infrastructure
{
    public interface IMessageListener
    {
        bool HandleCustomerExistResponse(SharedCustomer sharedCustomer);
    }
}
