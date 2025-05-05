using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class O_AniResimDegisimi : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> images;
    [SerializeField] private List<string> texts;
    [SerializeField] private float waitTime = 2f;
    private int currentIndex = 0;
    [SerializeField] private O_AniTopu aniTopuScript;
    [SerializeField] private Transform circleSpriteTransform;
    [SerializeField] private O_TypewriterEffect typewriterEffect;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private bool isTextComplete = false;
    void Awake()
    {
        typewriterEffect = GetComponent<O_TypewriterEffect>();
    }

    void OnEnable()
    {
        currentIndex = -1;
        circleSpriteTransform.DOScale(9f, 1f);
        StartCoroutine(ImageChangeCoroutine());
    }

    void Update()
    {
        // text tamamlandığında ve mouse tıklanırsa bir sonraki resme geç
        if (isTextComplete && Input.GetMouseButtonDown(0))
        {
            isTextComplete = false;
            images[currentIndex].DOFade(0, 0.5f).OnComplete(() =>
            {
                StartCoroutine(ImageChangeCoroutine());
            });
        }
    }
    private void AfterTextComplete()
    {
        isTextComplete = true;
    }
    private IEnumerator ImageChangeCoroutine()
    {
        currentIndex++;

        if(currentIndex == images.Count)
        {
            images[currentIndex - 1].DOFade(0, 1.35f);
            textMeshProUGUI.DOFade(0, 1.35f);
            textMeshProUGUI.transform.parent.GetComponent<Image>().DOFade(0, 1.35f);
            circleSpriteTransform.DOScale(21f, 1.5f).OnComplete(() =>
                {
                    aniTopuScript.ChangeScene();
                });
            yield break;
        }

        textMeshProUGUI.DOFade(0, .3f);
        images[currentIndex].DOFade(1, 0.5f).OnComplete(() =>
        {
            textMeshProUGUI.DOFade(1, 0.1f);
            typewriterEffect.StartTypewriter(texts[currentIndex], textMeshProUGUI, 0.05f, AfterTextComplete);
        });
    }
}
