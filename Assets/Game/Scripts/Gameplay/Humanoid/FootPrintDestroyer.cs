using UnityEngine;

public class FootPrintDestroyer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1f;
    private void Start()
    {
        Invoke(nameof(DestroyFootPrint), timeToDestroy);
    }

    private void DestroyFootPrint()
    {
        Destroy(this.gameObject);
    }
}
