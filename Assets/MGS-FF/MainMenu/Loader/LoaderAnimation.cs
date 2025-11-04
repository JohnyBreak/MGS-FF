using DG.Tweening;
using UnityEngine;

public class LoaderAnimation : MonoBehaviour
{
    void OnEnable()
    {
        transform
            .DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
