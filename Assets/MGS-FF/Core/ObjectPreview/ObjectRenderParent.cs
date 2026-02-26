using System;
using DG.Tweening;
using UnityEngine;

public class ObjectRenderParent : MonoBehaviour
{
    private const string RenderObject = "ObjectRender";
    [SerializeField] private Transform _objectParent;
    private Transform _objectToSet;
    
    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void SetObject(GameObject objectToSet)
    {
        objectToSet.transform.parent = _objectParent;
        objectToSet.layer = LayerMask.NameToLayer(RenderObject);
        objectToSet.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void StartAnim(Action endCallback)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_objectToSet.DOLocalRotate(new Vector3(0, 360, 0), 2, RotateMode.FastBeyond360));
        seq.AppendCallback(() => endCallback?.Invoke());
        seq.Play();
    }
}
