using EasyNetQ;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Infrastructure
{
    public class MessageListener
    {
        private IServiceProvider applicationServices;
        private string cloudAMQPConnectionString;

        public MessageListener(IServiceProvider applicationServices, string cloudAMQPConnectionString)
        {
            this.applicationServices = applicationServices;
            this.cloudAMQPConnectionString = cloudAMQPConnectionString;
        }
        public void Start()
        {
            using (IBus bus = RabbitHutch.CreateBus(cloudAMQPConnectionString))
            {
                bus.Subscribe<SharedOrderLine>("SharedOrderLineListener", HandleOrderLineResponse);
            }
        }

        private void HandleOrderLineResponse(SharedOrderLine sol)
        {
            throw new NotImplementedException();
        }
    }
}
