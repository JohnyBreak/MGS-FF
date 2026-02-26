using TMPro;
using UnityEngine;

public class PreviewView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Toggle(bool toggle)
    {
        gameObject.SetActive(toggle);
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
