using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    private const float MaxColorAlpha = 1f;
    private const float MinColorAlpha = 0f;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        SetCaretColorAlpha(MaxColorAlpha);
        NonNativeKeyboard.Instance.OnClosed += HideCaret;
    }

    private void HideCaret(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(MinColorAlpha);
        NonNativeKeyboard.Instance.OnClosed -= HideCaret;
    }

    void SetCaretColorAlpha(float value)
    {
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;
        inputField.caretColor = caretColor;
    }
}
