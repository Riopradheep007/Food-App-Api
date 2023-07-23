namespace foodDeliveryApi.Hubs
{
    public interface ISignalRHub
    {
        public Task DisplayMessage(string message);
    }
}
