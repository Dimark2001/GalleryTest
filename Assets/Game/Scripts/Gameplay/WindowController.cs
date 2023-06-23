using UnityEngine;
using UnityEngine.AddressableAssets;

public class WindowController : MonoBehaviour
{
    public bool IsVisible { get; private set; }

    protected CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void OnDestroy()
    {
        Addressables.ReleaseInstance(gameObject);
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
