using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GalleryWindow : WindowController
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject buttonPrefab;
    private List<GameObject> _photoCells;

    private void Awake()
    {
        StartCoroutine(DownloadImage("http://data.ikppbb.com/test-task-unity-data/pics/33.jpg"));
    }
    
    private IEnumerator DownloadImage(string mediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            Debug.LogError(request.error);
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            AddPhotoCell(Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.zero));
        }
    }
    
    private void AddPhotoCell(Sprite sprite)
    {
        var button = Instantiate(buttonPrefab, content);
        _photoCells ??= new List<GameObject>();
        _photoCells.Add(button);
        var buttonSprite = button.GetComponent<Image>();
        buttonSprite.sprite = sprite;
        buttonSprite.SetNativeSize();
        button.GetComponent<Button>().onClick.AddListener(delegate { OpenPhotoViewer(button.GetComponent<Image>().sprite); });
    }

    private void OpenPhotoViewer(Sprite sprite)
    {
        HideWindow();
        var photoViewer = WindowsManager.Instance.CreateWindow<PhotoViewerWindow>("PhotoViewerWindow");
        photoViewer.ShowWindow();
        photoViewer.SetPhotoSprite(sprite);
    }

    [SerializeField] private int urlPage; 
    [Button()]
    private void AddPhotoByURL()
    {
        StartCoroutine(DownloadImage("http://data.ikppbb.com/test-task-unity-data/pics/" + urlPage + ".jpg"));
    }
}
