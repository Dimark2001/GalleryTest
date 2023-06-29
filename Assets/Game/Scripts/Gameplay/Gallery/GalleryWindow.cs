using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class GalleryWindow : WindowController
{
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Scrollbar scrollbar;
    private bool _isAddNewPhoto = true;
    private bool _isPhotoEnded = false;
    private List<GameObject> _photoCells;

    private void Update()
    {
        if(!_isAddNewPhoto || _isPhotoEnded)
            return;

        if (scrollbar.size == 1)
        {
            _isAddNewPhoto = false;
            GetPhotoByNumber(1);
        }
        else if (scrollbar.value <= 0)
        {
            _isAddNewPhoto = false;
            GetSamePhoto(2);
        }
    }

    private void GetPhotoByNumber(int pageNumber)
    {
        WebRequests.GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/" + pageNumber + ".jpg", error =>
        {
            _isPhotoEnded = true;
            Debug.LogError(error);
        }, texture2D =>
        {
            AddPhotoCell(Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero));
            if (scrollbar.size == 1)
            {
                GetPhotoByNumber(content.childCount+1);
            }
            else
            {
                _isAddNewPhoto = true;
            }
        });
    }
    
    private void GetSamePhoto(int countPhoto)
    {
        var photoNumber = content.childCount + 1;
        WebRequests.GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/" + photoNumber + ".jpg", error =>
        {
            _isPhotoEnded = true;
            Debug.LogError(error);
        }, texture2D =>
        {
            AddPhotoCell(Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), Vector2.zero));
            if (countPhoto <= 1)
            {
                _isAddNewPhoto = true;
            }
            else
            {
                GetSamePhoto(countPhoto-1);
            }
        });
    }
    
    private void AddPhotoCell(Sprite sprite)
    {
        var button = Instantiate(buttonPrefab, content);
        _photoCells ??= new List<GameObject>();
        _photoCells.Add(button);
        var buttonSprite = button.GetComponent<Image>();
        buttonSprite.sprite = sprite;
        button.GetComponent<Button>().onClick.AddListener(delegate
        {
            OpenPhotoViewer(button.GetComponent<Image>().sprite);
        });
    }
    
    private void OpenPhotoViewer(Sprite sprite)
    {
        HideWindow();

        PhotoCash.Sprite = sprite;
        
        Loader.Load(Loader.Scene.PhotoViewer, 2f);
        /*var photoViewer = WindowsManager.Instance.CreateWindow<PhotoViewerWindow>("PhotoViewerWindow");
        photoViewer.ShowWindow();
        photoViewer.SetPhotoSprite(sprite);*/
    }
}
