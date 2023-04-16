using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace learnSignalr.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IHubContext<MessageHub> messageHub;
    private readonly IUserIdProvider customUserIdProvider;

    public WeatherForecastController(IHubContext<MessageHub> _messageHub,IUserIdProvider customUserIdProvider)
    {
        messageHub = _messageHub;
        this.customUserIdProvider = customUserIdProvider;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var WeatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return WeatherForecasts;
    }

    [HttpPost]
    [Route("productoffers")]
    public async Task<string> GetOffers([FromBody] Settings settings)
    {
        List<string> offers = new List<string>();
        offers.Add("20% Off on IPhone 12");
        offers.Add("15% Off on HP Pavillion");
        offers.Add("25% Off on Samsung Smart TV");

        var userId = settings.ConnectionId;

        try
        {
            if (!String.IsNullOrEmpty(settings.ConnectionId))
            {
                // messageHub.Clients.User(settings.ConnectionId).send
                var message = $"Send message to you with user id {userId}";
                await messageHub.Clients.User(userId).SendAsync("SendOffersToUser", message);
            }
            else
            {
                await messageHub.Clients.All.SendAsync("SendOffersToUser", "This is the message");
            }
        }
        catch (System.Exception ex)
        {
            // TODO
        }


        return "Offers sent successfully to all users!";
    }

    public class Settings
    {
        public string ConnectionId { get; set; }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.ConnectionId;
        }
    }
}
