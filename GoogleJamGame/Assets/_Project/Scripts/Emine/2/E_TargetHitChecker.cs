using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class E_TargetHitChecker : MonoBehaviour
{
    public Button actionButton;
    public Slider successSlider;
    public GameObject dialogue;
    public TextMeshProUGUI dialogueText;               // Baþarýlý olduðunda çýkan metin
    public CanvasGroup dialogueCanvasGroup;

    public TextMeshProUGUI failText;                   // Baþarýsýz týklama için kýsa metin
    public CanvasGroup failCanvasGroup;

    public E_FloatUpDown floatScript;  // Hareketi durduracaðýmýz script

    public float sliderIncreaseAmount = 1f;
    public float fillSpeed = 2f;
    public float fadeDuration = 0.5f;
    public float displayTime = 1.5f;

    public string[] successMessages;
    private int currentMessageIndex = 0;

    private bool inZone = false;
    private float targetSliderValue;

    public AudioSource audioSource;
    public AudioClip call1Sound;
    public AudioClip call2Sound;
    public AudioClip tikSound;
    public AudioClip tokSound;
    public AudioClip failSound;
    public AudioClip winSound;

    bool hasWon = false;

    public RectTransform imageRectTransform;
    public Vector3 targetPosition; // Genellikle sahnenin ortasý
    public Vector3 targetScale = Vector3.one; // Normal boyut
    public float moveDuration = 1.5f;

    IEnumerator MoveAndScaleImage()
    {
        Vector3 startPosition = imageRectTransform.anchoredPosition;
        Vector3 startScale = imageRectTransform.localScale;

        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);

            // Pozisyon ve ölçek interpolasyonu
            imageRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            imageRectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        // Tam hedefe oturt
        imageRectTransform.anchoredPosition = targetPosition;
        imageRectTransform.localScale = targetScale;
    }


    void Start()
    {
        dialogue.SetActive(false);
        targetSliderValue = successSlider.value;

        // Baþlangýçta uyarý metni görünmesin
        failText.gameObject.SetActive(false);
        failCanvasGroup.alpha = 0;

        StartCoroutine(PlaySoundsLoop());
    }

    void Update()
    {
        successSlider.value = Mathf.MoveTowards(successSlider.value, targetSliderValue, fillSpeed * Time.deltaTime);

        if (targetSliderValue == successSlider.maxValue)
        {    
            actionButton.interactable = false;
            floatScript.isMovementPaused = true;
            hasWon = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TargetZone"))
            inZone = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("TargetZone"))
            inZone = false;
    }

    public void Call()
    {
        actionButton.interactable = false;
        floatScript.isMovementPaused = true;

        if (inZone)
        {
            audioSource.PlayOneShot(call1Sound);
            Debug.Log("Baþarýlý!");
            targetSliderValue += sliderIncreaseAmount;
            if (targetSliderValue > successSlider.maxValue)
                targetSliderValue = successSlider.maxValue;

            ShowSuccessDialogue();
        }
        else
        {
            Debug.Log("Kaçýrdýn.");
            targetSliderValue -= sliderIncreaseAmount;
            if (targetSliderValue < successSlider.minValue)
                targetSliderValue = successSlider.minValue;
            ShowFailMessage();
        }
    }

    void ShowSuccessDialogue()
    {
        if (successMessages.Length > 0)
        {
            dialogueText.text = successMessages[currentMessageIndex];
            currentMessageIndex = (currentMessageIndex + 1) % successMessages.Length;
        }

        StopAllCoroutines();
        StartCoroutine(FadeCanvas(dialogueCanvasGroup, dialogue, true));
    }

    void ShowFailMessage()
    {
        audioSource.PlayOneShot(failSound);
        failText.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(failCanvasGroup, failText.gameObject, false));
        
    }

    IEnumerator FadeCanvas(CanvasGroup group, GameObject obj, bool deactivateAfter)
    {     
        obj.SetActive(true);
        group.alpha = 0;

        float t = 0;
        while (t < fadeDuration)
        {
            group.alpha = Mathf.Lerp(0, 1, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        group.alpha = 1;

        yield return new WaitForSeconds(displayTime);

        t = 0;
        while (t < fadeDuration)
        {
            group.alpha = Mathf.Lerp(1, 0, t / fadeDuration);
            t += Time.deltaTime;
            yield return null;
        }
        group.alpha = 0;

        if (deactivateAfter)
            obj.SetActive(false);

        actionButton.interactable = true;
        floatScript.isMovementPaused = false;
        if (group == dialogueCanvasGroup)
        {
            audioSource.PlayOneShot(call2Sound);
        }
        StartCoroutine(PlaySoundsLoop());

        if (hasWon)
        {
            StopAllCoroutines();
            StartCoroutine(MoveAndScaleImage());
            audioSource.PlayOneShot(winSound);
        }
    }

    IEnumerator PlaySoundsLoop()
    {
        while (true)
        {
            audioSource.PlayOneShot(tikSound);
            yield return new WaitForSeconds(tikSound.length + 0.5F);

            audioSource.PlayOneShot(tokSound);
            yield return new WaitForSeconds(tokSound.length + 0.5F);
        }
    }
}
