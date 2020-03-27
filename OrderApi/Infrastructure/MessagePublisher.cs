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
        IBus bus;

        public MessagePublisher(IServiceProvider applicationServices, string cloudAMQPConnectionString)
        {
            this.applicationServices = applicationServices;
            this.cloudAMQPConnectionString = cloudAMQPConnectionString;
        }

        public void Start()
        {
           // using (bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
        }
        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishCustomerExists(HiddenOrder order)
        {
            using (bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
            {
                bus.Publish(new SharedCustomer
                {
                    Id = order.customerId
                }, "OrderPublisherCustomerApi");
            }
        }
    }
}
