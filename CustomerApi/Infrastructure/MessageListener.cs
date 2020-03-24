using CustomerApi.Data;
using CustomerApi.HiddenModel;
using EasyNetQ;
using RestSharp;
using SharedModels;
using System;

namespace CustomerApi.Infrastructure
{
    public class MessageListener
    {
        IServiceProvider provider;
        string connectionString;
        public MessageListener(IServiceProvider provider, string connectionString)
        {
            this.provider = provider;
            this.connectionString = connectionString;
        }

        public void Start()
        {
            using (IBus bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.Subscribe<SharedCustomer>("OrderPublisherCustomerApi", message => HandleCustomerReply(message));
            }
        }

        private void HandleCustomerReply(SharedCustomer customer)
        {
            RestClient c = new RestClient();

            c.BaseUrl = new Uri("https://localhost:5001/products");
            RestRequest request = new RestRequest(customer.Id.ToString(), Method.GET);
            IRestResponse<HiddenCustomer> response = c.Execute<HiddenCustomer>(request);
            HiddenCustomer getCustomer = response.Data;

            if(getCustomer.Id == null || getCustomer.Id < 1)
            {
                //Use MessagePublisher to publish a message in the bus and receive it in the order api
            }
            
        }
    }
}
