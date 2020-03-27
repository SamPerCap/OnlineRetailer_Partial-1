using CustomerApi.HiddenModel;
using EasyNetQ;
using SharedModels;
using System;

namespace CustomerApi.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        private IServiceProvider applicationServices;
        private string cloudAMQPConnectionString;
        public MessagePublisher(IServiceProvider applicationServices, string cloudAMQPConnectionString)
        {
            this.applicationServices = applicationServices;
            this.cloudAMQPConnectionString = cloudAMQPConnectionString;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ResponseCustomerExists(HiddenCustomer customer)
        {
            using (IBus bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
            {
                bus.Publish(new SharedCustomer
                {
                    Id = customer.Id
                }, "ResponseCustomerExists");
            }
        }
    }
}
