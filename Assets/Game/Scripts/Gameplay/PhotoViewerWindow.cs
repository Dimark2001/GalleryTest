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
    }

    public void SetPhotoSprite(Sprite sprite)
    {
        photoRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 1080);
        photo.sprite = sprite;
    }

    private void BackToGallery()
    {
        HideWindow();
        WindowsManager.Instance.CreateWindow<GalleryWindow>("GalleryWindow").ShowWindow();
    }
}
