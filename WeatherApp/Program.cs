using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    // Reutilizar una sola instancia de HttpClient (Buena práctica del Material 8)
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        // NOTA: Aquí irá tu API Key de OpenWeather cuando la generes
        string apikey = "TU_API_KEY_AQUI";
        string ciudad = "Mexico City";

        // Construcción de la URL con los parámetros requeridos
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={apikey}&units=metric&lang=es";

        try
        {
            // Realizar la solicitud GET de forma asíncrona
            HttpResponseMessage response = await client.GetAsync(url);

            // Lanzar excepción si el código de estado indica error (ej. 401 o 404)
            response.EnsureSuccessStatusCode();

            // Leer el contenido de la respuesta como string
            string jsonRespuesta = await response.Content.ReadAsStringAsync();

            // Parsear el JSON con Newtonsoft.Json
            JObject datos = JObject.Parse(jsonRespuesta);

            // Extraer los datos relevantes del JSON
            string descripcion = datos["weather"][0]["description"].ToString();
            double temp = (double)datos["main"]["temp"];
            double sensacion = (double)datos["main"]["feels_like"];
            int humedad = (int)datos["main"]["humidity"];
            double viento = (double)datos["wind"]["speed"];

            // Mostrar los resultados en la consola
            Console.WriteLine($"=== Clima en {ciudad} ===");
            Console.WriteLine($"Descripción: {descripcion}");
            Console.WriteLine($"Temperatura: {temp}°C");
            Console.WriteLine($"Sensación: {sensacion}°C");
            Console.WriteLine($"Humedad: {humedad}%");
            Console.WriteLine($"Viento: {viento} m/s");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
    }
}