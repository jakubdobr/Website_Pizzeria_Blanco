using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Api.Services
{
    public class AddressVerificationService : IAddressVerificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly (double Latitude, double Longitude) _pizzeriaLocation = (52.2297, 21.0122); // Przykładowe współrzędne

        public AddressVerificationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleMaps:ApiKey"]; // Pobieranie klucza z konfiguracji

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("Google Maps API key is not set.");
            }
        }

        public async Task<bool> VerifyAddressAsync(string address)
        {
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var geoResponse = JsonSerializer.Deserialize<GeoResponse>(content);
                return geoResponse.Status == "OK";
            }
            return false;
        }

        public async Task<bool> IsWithinDeliveryRangeAsync(string destinationAddress, double maxDistanceKm)
        {
            var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={_pizzeriaLocation.Latitude},{_pizzeriaLocation.Longitude}&destinations={Uri.EscapeDataString(destinationAddress)}&key={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var distanceMatrixResponse = JsonSerializer.Deserialize<DistanceMatrixResponse>(content);

                if (distanceMatrixResponse.Rows[0].Elements[0].Status == "OK")
                {
                    var distanceInMeters = distanceMatrixResponse.Rows[0].Elements[0].Distance.Value;
                    var distanceInKm = distanceInMeters / 1000.0;

                    return distanceInKm <= maxDistanceKm;
                }
            }

            return false;
        }
    }

    public class GeoResponse
    {
        public string Status { get; set; }
        public List<GeoResult> Results { get; set; }
    }

    public class GeoResult
    {
        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class DistanceMatrixResponse
    {
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        public List<Element> Elements { get; set; }
    }

    public class Element
    {
        public Distance Distance { get; set; }
        public string Status { get; set; }
    }

    public class Distance
    {
        public int Value { get; set; } // Wartość w metrach
    }
}
