using ChattingApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ChattingApp.Services;

public interface IChatService
{
    Task SendPrivateMessageAsync(string fromUserId, string toUserId, string message);
    Task SendNotificationAsync(string toUserId, string title, string message);
}

// Concrete implementation
public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _chatHubContext;

    public ChatService(IHubContext<ChatHub> chatHubContext)
    {
        _chatHubContext = chatHubContext;
    }

    public async Task SendPrivateMessageAsync(string fromUserId, string toUserId, string message)
    {
        await _chatHubContext.Clients.User(toUserId).SendAsync("ReceivePrivateMessage", fromUserId, message);
    }
    public async Task SendNotificationAsync(string toUserId, string title, string message)
    {
        await _chatHubContext.Clients.User(toUserId).SendAsync("ReceiveNotification", title, message);
    }
}

