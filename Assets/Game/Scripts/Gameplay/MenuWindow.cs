using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : WindowController
{
    [SerializeField] private Button galleryButton;

    private void Awake()
    {
        galleryButton.onClick.AddListener(OpenGallery);
    }

    private void OpenGallery()
    {
        HideWindow();
        var galleryWindow = WindowsManager.Instance.CreateWindow<GalleryWindow>("GalleryWindow");
        galleryWindow.ShowWindow();
    }
}
