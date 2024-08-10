using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : Singleton<APIManager>
{
    private const string BASE_URL = "http://localhost/anyplay-worktest-php/public/";

    private IEnumerator SendRequest(string endpoint, string httpMethod, string jsonPayload, Action<string> onSuccess, Action<string> onError)
    {
        if (IsNetworkAvailable())
        {
            string url = BASE_URL + endpoint;
            Debug.Log($"{httpMethod}\n{url}\n{jsonPayload}");
            UnityWebRequest request = null;

            switch (httpMethod)
            {
                case "GET":
                    request = UnityWebRequest.Get(url);
                    break;
                case "PUT":
                case "POST":
                    byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
                    request = new UnityWebRequest(url, httpMethod);
                    request.uploadHandler = new UploadHandlerRaw(jsonBytes);
                    request.SetRequestHeader("Content-Type", "application/json");
                    break;
                case "DELETE":
                    request = UnityWebRequest.Delete(url);
                    break;
                default:
                    onError?.Invoke("Unsupported HTTP method");
                    break;
            }

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log($"Error : {endpoint}\n{jsonPayload}");
                onError?.Invoke(request.downloadHandler.text);
            }
            else
            {
                Debug.Log($"Success : {endpoint}\n{jsonPayload}");
                onSuccess?.Invoke(request.downloadHandler.text);
            }
        }
        else
        {
            Debug.Log("Network not Available");
        }
    }

    public void Get(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(GetRoutine(endpoint, onSuccess, onError));
    }

    public void Post(string endpoint, string jsonPayload, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(PostRoutine(endpoint, jsonPayload, onSuccess, onError));
    }

    public void Put(string endpoint, string jsonPayload, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(PutRoutine(endpoint, jsonPayload, onSuccess, onError));
    }

    public void Delete(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        StartCoroutine(DeleteRoutine(endpoint, onSuccess, onError));
    }

    public IEnumerator GetRoutine(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        return SendRequest(endpoint, "GET", string.Empty, onSuccess, onError);
    }

    public IEnumerator PostRoutine(string endpoint, string jsonPayload, Action<string> onSuccess, Action<string> onError)
    {
        return SendRequest(endpoint, "POST", jsonPayload, onSuccess, onError);
    }

    public IEnumerator PutRoutine(string endpoint, string jsonPayload, Action<string> onSuccess, Action<string> onError)
    {
        return SendRequest(endpoint, "PUT", jsonPayload, onSuccess, onError);
    }

    public IEnumerator DeleteRoutine(string endpoint, Action<string> onSuccess, Action<string> onError)
    {
        return SendRequest(endpoint, "DELETE", string.Empty, onSuccess, onError);
    }

    private bool IsNetworkAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

}
