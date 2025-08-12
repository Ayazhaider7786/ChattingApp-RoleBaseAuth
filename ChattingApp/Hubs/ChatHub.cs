using ChattingApp.Services;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChattingApp.Hubs;

public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task SendPrivateMessage(string fromUserId, string toUserId, string message)
    {
        await _chatService.SendPrivateMessageAsync(fromUserId, toUserId, message);
        await _chatService.SendNotificationAsync(toUserId, "New Message", $"Message From {toUserId}");
    }
    public async Task SendNotification(string toUserId, string title, string message)
    {
        await _chatService.SendNotificationAsync(toUserId, title, message);
    }
}
