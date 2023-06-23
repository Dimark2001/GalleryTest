using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(CanvasGroup))]
public class WindowController : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    protected CanvasGroup _canvasGroup;

    protected virtual void OnDestroy()
    {
        //Addressables.ReleaseInstance(gameObject);
    }

    public void OpenWindow()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        IsVisible = true; 
    }
    
    public void ShowWindow(bool withAnimation = true)
    {
        if (IsVisible) return;

        IsVisible = true;

        _canvasGroup.alpha = 1;
        SetInteractable(true);
    }

    public void HideWindow(bool withAnimation = true)
    {
        if (IsVisible == false) return;

        IsVisible = false;

        _canvasGroup.alpha = 0;
        SetInteractable(false);
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }

    public void SetInteractable(bool state)
    {
        _canvasGroup.blocksRaycasts = state;
    }

    private void AddToWindowManager()
    {
    }
}
