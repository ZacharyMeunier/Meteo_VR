using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Unity.VisualScripting;
public class MeteoManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI temperatureText;
    public GameObject sun;
    public GameObject snow;
    public GameObject rain;
    public GameObject cloud;

    void Start()
    {
        StartCoroutine(GetWeather());
    }
    IEnumerator GetWeather()
    {
        string url = "https://api.open-meteo.com/v1/forecast?latitude=45.50&longitude=-73.56&current_weather=true";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            WeatherData data = JsonUtility.FromJson<WeatherData>(request.downloadHandler.text);
            float temp = data.current_weather.temperature;
            int code = data.current_weather.weathercode;
            temperatureText.text = temp + "°C & code = " + code;

            // Activation simple selon le code
            snow.SetActive(code >= 71 && code <= 77);
            snow.SetActive(code >= 0 && code <= 3);
            //sun.SetActive(code >= 0 && code <= 3);
            //cloud.SetActive(code >= 1 && code <= 3);
            rain.SetActive(code >= 61 && code <= 65);
        }
        else
        {
            Debug.LogError(request.error);
        }
    }
}
[System.Serializable]
public class WeatherData
{
    public CurrentWeather current_weather;
}
[System.Serializable]
public class CurrentWeather
{
    public float temperature;
    public int weathercode;
}
