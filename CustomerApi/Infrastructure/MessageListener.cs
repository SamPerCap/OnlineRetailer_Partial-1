using CustomerApi.Data;
using CustomerApi.HiddenModel;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SharedModels;
using System;
using System.Threading;

namespace CustomerApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        readonly IMessagePublisher messagePublisher;

        public MessageListener(IServiceProvider provider, string connectionString, IMessagePublisher messagePublisher)
        {
            this.provider = provider;
            this.connectionString = connectionString;
            this.messagePublisher = messagePublisher;
        }

        public void Start()
        {
            using (IBus bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.Subscribe<SharedCustomer>("OrderPublisherCustomerApi", message => HandleCustomerReply(message));

                lock (this)
                {
                    Monitor.Wait(this);
                }
            }
        }

        private void HandleCustomerReply(SharedCustomer customer)
        {
            using (var scope = provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var customerRepo = services.GetService<IRepository<SharedCustomer>>();

                var getCustomer = customerRepo.Get(customer.Id);
                messagePublisher.ResponseCustomerExists(new HiddenCustomer
                {
                    Id = getCustomer.Id
                });
                /*RestClient c = new RestClient();
                c.BaseUrl = new Uri("https://localhost:5001/products");
                RestRequest request = new RestRequest(customer.Id.ToString(), Method.GET);
                IRestResponse<HiddenCustomer> response = c.Execute<HiddenCustomer>(request);
                HiddenCustomer getCustomer = response.Data;
                messagePublisher.ResponseCustomerExists(getCustomer);*/
            }
        }
    }
}
