using Common.Model.Customer;

namespace foodDeliveryApi.Hubs
{
    public interface ISignalRHub
    {
        public Task DisplayMessage(string message);
        public Task BroadCastSpicesData(Foods data);
        public Task BroadCastJuiceData(Foods data);
        public  Task BroadCastIceCreamData(Foods data);
    }
}
