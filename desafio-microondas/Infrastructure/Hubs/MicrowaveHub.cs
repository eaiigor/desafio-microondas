using Microsoft.AspNetCore.SignalR;

namespace desafio_microondas.Infrastructure.Hubs;

public class MicrowaveHub : Hub
{
    public async Task JoinMicrowaveGroup(string microwaveId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, microwaveId);
    }
    
    public async Task LeaveMicrowaveGroup(string microwaveId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, microwaveId);
    }
}