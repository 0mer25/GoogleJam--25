using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class O_TypewriterEffect : MonoBehaviour
{
    public void StartTypewriter(string message, TextMeshProUGUI textUI, float delay = 0.05f, Action OnComplete = null)
    {
        StartCoroutine(TypeTextCoroutine(message, textUI, delay, OnComplete));
    }

    private IEnumerator TypeTextCoroutine(string message, TextMeshProUGUI textUI, float delay, Action OnComplete = null)
    {
        textUI.text = "";
        for (int i = 0; i < message.Length; i++)
        {
            textUI.text += message[i];
            yield return new WaitForSeconds(delay);
        }

        Debug.Log("Yazı tamamlandı: " + message);
        OnComplete?.Invoke();
    }
}
