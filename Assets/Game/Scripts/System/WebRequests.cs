using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public static class WebRequests
{
    #region Public
    
    public static void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        _webRequestsMonoBehavior.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }
    
    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        _webRequestsMonoBehavior.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }
    
    public static void GetAudioClip(string url, AudioType type, Action<string> onError, Action<AudioClip> onSuccess)
    {
        Init();
        _webRequestsMonoBehavior.StartCoroutine(GetAudioClipCoroutine(url, type, onError, onSuccess));
    }
    
    #endregion
    
    private class WebRequestsMonoBehavior : MonoBehaviour{}

    private static WebRequestsMonoBehavior _webRequestsMonoBehavior;

    private static void Init()
    {
        if (_webRequestsMonoBehavior == null)
        {
            GameObject gameObject = new GameObject();
            _webRequestsMonoBehavior = gameObject.AddComponent<WebRequestsMonoBehavior>();
        }
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess)
    {
        using var unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result is UnityWebRequest.Result.ConnectionError or not UnityWebRequest.Result.Success)
            onError(unityWebRequest.error);
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        using var unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result is UnityWebRequest.Result.ConnectionError or not UnityWebRequest.Result.Success)
            onError(unityWebRequest.error);
        else
        {
            var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
            onSuccess(texture);
        }
    }
    
    private static IEnumerator GetAudioClipCoroutine(string url, AudioType type, Action<string> onError, Action<AudioClip> onSuccess)
    {
        using var unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(url, type);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result is UnityWebRequest.Result.ConnectionError or not UnityWebRequest.Result.Success)
            onError(unityWebRequest.error);
        else
        {
            var clip = DownloadHandlerAudioClip.GetContent(unityWebRequest);
            onSuccess(clip);
        }
    }
}
