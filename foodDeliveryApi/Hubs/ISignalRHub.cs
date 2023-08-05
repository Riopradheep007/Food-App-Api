using Common.Model.Customer;
using Common.Model.Restaurent;

namespace foodDeliveryApi.Hubs
{
    public interface ISignalRHub
    {
        public Task DisplayMessage(string message);
        public Task BroadCastSpicesData(Foods data);
        public Task BroadCastJuiceData(Foods data);
        public  Task BroadCastIceCreamData(Foods data);
        public Task SendCustomerOrders(IList<CustomerOrders> orders);
    }
}
