using Microsoft.AspNetCore.SignalR;

namespace foodDeliveryApi.Hubs
{
    public class SignalRHub:Hub<ISignalRHub>
    {
        public void Hello()
        {
            Clients.Caller.DisplayMessage("Hello from the SignalrDemoHub!");
        }
    }
}
