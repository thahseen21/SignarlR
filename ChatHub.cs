using System;
using Microsoft.AspNetCore.SignalR;

namespace learnSignalr
{
    public class MessageHub : Hub
    {
        // public async Task SendOffersToUser(List<string> message)
        // {
        //     await Clients.All.SendAsync("ALL User");
        // }

        // public async Task Send(string userId)
        // {
        //     var message = $"Send message to you with user id {userId}";
        //     await Clients.Client(userId).SendAsync("ReceiveMessage", message);
        // }

    }

    // public interface IMessageHubClient
    // {
    //     Task SendOffersToUser(List<string> message);
    // }
}
