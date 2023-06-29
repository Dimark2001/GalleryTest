using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoaderWinidow : WindowController
{
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private TextMeshProUGUI progressText;

    private void Start()
    {
        var inVal = 0f;
        DOTween.To(() => inVal, x => inVal = x, 1f, 2f).OnUpdate((() =>
        {
            scrollbar.size = inVal;
            progressText.text = (int)(inVal*100) + "%";
        }));
        
    }
}
