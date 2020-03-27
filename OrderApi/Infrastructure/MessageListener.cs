using EasyNetQ;
using SharedModels;
using System;
using System.Threading;

namespace OrderApi.Infrastructure
{
    public class MessageListener : IMessageListener
    {
        private IServiceProvider applicationServices;
        private string cloudAMQPConnectionString;
        readonly static int timeoutInterval = 2000;


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
                bus.Subscribe<SharedCustomer>("ResponseCustomerExists", message => HandleCustomerExistResponse(message));

                lock (this)
                    Monitor.Wait(this);
            }
        }
        public bool HandleCustomerExistResponse(SharedCustomer sharedCustomer)
        {

            // Do something to return true or false if customer exist
            if (sharedCustomer == null || sharedCustomer.Id < 1)
                return false;
            else
                return true;

        }

        private void HandleOrderLineResponse(SharedOrderLine sol)
        {
            throw new NotImplementedException();
        }
    }
}
