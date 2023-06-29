using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowsManager : SingletonPersistent<WindowsManager>
{
    [SerializeField] private List<Transform> initWindows;
    
    private List<WindowController> _createdWindows;
    private Canvas _canvas;
    public override void Awake()
    {
        base.Awake();
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == Loader.Scene.Menu.ToString())
            CreateWindow<MenuWindow>("MenuWindow");
        if(SceneManager.GetActiveScene().name == Loader.Scene.Gallery.ToString())
            CreateWindow<GalleryWindow>("GalleryWindow");
        if(SceneManager.GetActiveScene().name == Loader.Scene.PhotoViewer.ToString())
            CreateWindow<PhotoViewerWindow>("PhotoViewerWindow");
        _canvas.worldCamera = Camera.main;
    }

    public T CreateWindow<T>(string windowName) where T : WindowController
    {
        var createdWindow = SearchCreatedWindow<T>(windowName);
        
        if (createdWindow == null)
        {
            var initWindow = SearchInitWindow<T>(windowName);
            if(initWindow == null) Debug.LogError("Window not found");
            createdWindow = Instantiate(initWindow, _canvas.transform);
            createdWindow.OpenWindow();
            _createdWindows.Add(createdWindow);
        }

        return createdWindow;
    }

    public void CloseAllWindows()
    {
        foreach (var window in _createdWindows)
        {
            if (window.TryGetComponent(out WindowController windowController))
            {
                windowController.CloseWindow();
            }
        }
    }
    
    public T SearchInitWindow<T>(string windowName) where T : WindowController
    {
        T targetWindow = null;

        foreach (var initWindow in initWindows)
        {
            if (!initWindow.TryGetComponent(out T windowComponent)) continue;
            var createdWindowName = initWindow.name;
            createdWindowName = createdWindowName.Replace("(Clone)", "");
            if (createdWindowName == windowName)
            {
                targetWindow = windowComponent;
                break;
            }
        }

        return targetWindow;
    }
    
    public T SearchCreatedWindow<T>(string windowName) where T : WindowController
    {
        T targetWindow = null;
        _createdWindows ??= new List<WindowController>();
        foreach (var createdWindow in _createdWindows)
        {
            var createdWindowName = createdWindow.name;
            createdWindowName = createdWindowName.Replace("(Clone)", "");
            if (createdWindowName == windowName)
            {
                targetWindow = (T)createdWindow;
                break;
            }
        }

        return targetWindow;
    }
}