using System;
using UnityEngine;
using UnityEngine.UI;

public class PhotoViewerWindow : WindowController
{
    [SerializeField] private RectTransform photoRectTransform;
    [SerializeField] private Image photo;
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(BackToGallery);
        SetPhotoSprite(PhotoCash.Sprite);
    }

    public void SetPhotoSprite(Sprite sprite)
    {
        //photoRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.width);
        photo.sprite = sprite;
    }

    private void BackToGallery()
    {
        HideWindow();
        Loader.Load(Loader.Scene.Gallery, 2f);
        //WindowsManager.Instance.CreateWindow<GalleryWindow>("GalleryWindow").ShowWindow();
    }
}
