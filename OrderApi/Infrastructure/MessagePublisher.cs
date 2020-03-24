using EasyNetQ;
using OrderApi.Models;
using SharedModels;
using System;

namespace OrderApi.Infrastructure
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

        public void Start()
        {
            using (IBus bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
            {
                bus.Publish(new SharedOrderLine
                {

                });
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void PublishCustomerExists(HiddenOrder order)
        {
            using (IBus bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
            {
                bus.Publish(new SharedCustomer
                {
                    Id = order.customerId
                }, "OrderPublisherCustomerApi");
            }
        }
    }
}
