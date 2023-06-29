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
        Loader.Load(Loader.Scene.Gallery, 2f);
        /*var galleryWindow = WindowsManager.Instance.CreateWindow<GalleryWindow>("GalleryWindow");
        galleryWindow.ShowWindow();*/
    }
}
