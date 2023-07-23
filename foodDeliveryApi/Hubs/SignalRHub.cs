using Common.Model.Customer;
using Microsoft.AspNetCore.SignalR;

namespace foodDeliveryApi.Hubs
{
    public class SignalRHub:Hub<ISignalRHub>
    {
        private IHubContext<SignalRHub> _hubcontext;

        public SignalRHub(IHubContext<SignalRHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

        public void Hello()
        {
            Clients.Caller.DisplayMessage("Hello from the SignalrDemoHub!");
        }
        public async Task BroadCastSpicesData(IList<Foods> data) {
            await _hubcontext.Clients.All.SendAsync("Spices", data);
        }
        public async Task BroadCastJuiceData(IList<Foods> data)
        {
            await _hubcontext.Clients.All.SendAsync("Juice", data);
        }
        public async Task BroadCastIceCreamData(IList<Foods> data)
        {
            await _hubcontext.Clients.All.SendAsync("IceCream", data);
        }
    }
}
