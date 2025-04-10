using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


var client = DeviceClient.CreateFromConnectionString(args[0]);

var i = 0;

while (true)
{
    var obj = new
    {
        event_id = Guid.NewGuid().ToString("N"),
        device_id = "device20250410",
        event_produced_utc = DateTime.UtcNow,
        form = "liquid",
        temperature = 20.05D + (i),
        temperature_unit = "C",
        humidity = 0.5D,
        humidity_unit = "RH",
        pressure = 6000.00D + i * 100,
        pressure_unit = "kPa"
    };

    var json = JsonSerializer.Serialize(obj);
    var msg = new Message(Encoding.UTF8.GetBytes(json));

    await client.SendEventAsync(msg);

    Console.WriteLine(json);

    await Task.Delay(10000);

    i++;
}