using MonitoringDumpTruck.Models.Entities;
using System.Text;
using System.Text.Json;

namespace MonitoringDumpTruck.Helpers;

public class FlaskApiService
{
    private readonly HttpClient _httpClient;
    public FlaskApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetEnginePrediction(Pointer pointer)
    {
        //var queryString = $"?rpm={pointer.EngineSpeed}&temperature={pointer.EngineTemperature}" +
        //    $"&pressure={pointer.EnginePressure}&vibration={pointer.EngineVibration}&fuel={pointer.Fuel}" +
        //    $"&speed={pointer.Speed}&load={pointer.EngineLoad}";

        //var response = await _httpClient.GetAsync($"http://localhost:5055/engine/predict{queryString}");


        var jsonContent = new StringContent(JsonSerializer.Serialize(pointer), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5055/engine/predict", jsonContent);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetTirePrediction(Pointer pointer)
    {
        double[] features = new double[] 
        { 
            pointer.TirePressure, 
            pointer.TireTemperature,
            pointer.TireTreadDepth,
            pointer.Speed,
            pointer.RoadTypeId
        };

        var requestContent = new
        {
            features = features
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(requestContent), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:5055/tire/predict", jsonContent);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
