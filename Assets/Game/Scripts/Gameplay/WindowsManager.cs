using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : SingletonPersistent<WindowsManager>
{
    [SerializeField] private List<Transform> initWindows;
    private List<Transform> _createdWindows;

    public T CreateWindow<T>(string windowName) where T : WindowController
    {
        var createdWindow = SearchForWindow<T>(windowName);
        
        if (createdWindow == null)
        {
            Debug.Log("createdWindow == null");
            return null;
        }

        _createdWindows ??= new List<Transform>();
        _createdWindows.Add(createdWindow.transform);
        createdWindow.ShowWindow();

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

    public T SearchForWindow<T>(string windowName) where T : WindowController
    {
        T targetWindow = null;

        foreach (var createdWindow in initWindows)
        {
            if (!createdWindow.TryGetComponent(out T windowComponent)) continue;

            var createdWindowName = createdWindow.name;
            //createdWindowName = createdWindowName.Replace("(Clone)", "");

            if (createdWindowName == windowName)
            {
                targetWindow = windowComponent;
                break;
            }
        }

        return targetWindow;
    }
}