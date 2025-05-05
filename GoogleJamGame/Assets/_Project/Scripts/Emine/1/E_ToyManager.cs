using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class E_ToyManager : MonoBehaviour
{
    public GameObject fixToy;
    public SpriteRenderer toy;
    public Sprite brokeToy;
    public Sprite fixedToy;
    private E_GameManager gameManager;

    public GameObject particleEffectPrefab;
    bool effectPlayed = false;

    public TextMeshProUGUI text1;  
    public TextMeshProUGUI text2;  
    public TextMeshProUGUI text3;  
    

    public AudioSource audioSource;
    public AudioClip collectSound;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<E_GameManager>();
        fixToy.SetActive(false);
        toy.sprite = brokeToy;

        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (gameManager.scoreComlpleted)
        {
            isText3Active = true;
            StartCoroutine(FadeInFixToy());
        }
    }

    // Text1 ve Text2 i�in aktif durumu kontrol eden de�i�kenler
    private bool isText1Active = false;
    private bool isText2Active = false;
    private bool isText3Active = false;
    

    void OnMouseEnter()
    {
        if (!gameManager.scoreComlpleted)
        {
            if (gameManager.score == 5 && !isText2Active)
            {
                ShowText(text2);  // score 5 ise text2'yi g�ster
            }
            else if (!isText1Active)
            {
                ShowText(text1);  // Aksi takdirde text1'i g�ster
            }
        }
        else
        {
            if (!isText3Active)
            {
                ShowText(text3);
            }
            
        }
    }

    void OnMouseExit()
    {
        if (!gameManager.scoreComlpleted)
        {
            if (gameManager.score == 5 && isText2Active)
            {
                HideText(text2);  // score 5 ise text2'yi gizle
            }
            else if (isText1Active)
            {
                HideText(text1);  // text1'i gizle
            }
        }
        else
        {
            HideText(text3);
        }
    }

    void ShowText(TextMeshProUGUI text)
    {
        if (text == text1)
        {
            isText1Active = true;  // text1 aktif oldu
        }
        else if (text == text2)
        {
            isText2Active = true;  // text2 aktif oldu
        }

        text.gameObject.SetActive(true);  // Text'i hemen aktif et
    }

    void HideText(TextMeshProUGUI text)
    {
        if (text == text1)
        {
            isText1Active = false;  // text1 gizlendi
        }
        else if (text == text2)
        {
            isText2Active = false;  // text2 gizlendi
        }

        text.gameObject.SetActive(false);  // Text'i kapat
    }



    void Update()
    {
        if (gameManager.fixScoreCompleted && !effectPlayed)
        {
            GameObject effect = Instantiate(particleEffectPrefab, Vector3.zero, Quaternion.identity);

            toy.sprite = fixedToy;

            effectPlayed = true; // bir daha �al��mas�n

            //iki saniye sonra fixtoy yava��a silikle�sin ve kapans�n istiyorum
            StartCoroutine(FadeAndDisableFixToy());
        }
    }

    IEnumerator FadeInFixToy()
    {
        fixToy.SetActive(true); // fixToy'u aktif hale getiriyoruz
        
        SpriteRenderer[] sprites = fixToy.GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1f;
        float elapsed = 0f;

        // Orijinal renkleri sakla
        Color[] originalColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color;
        }

        // Ba�lang��ta t�m sprite'lar tam �effaf (alpha = 0)
        foreach (var sprite in sprites)
        {
            sprite.color = new Color(originalColors[0].r, originalColors[0].g, originalColors[0].b, 0f);
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);

            for (int i = 0; i < sprites.Length; i++)
            {
                Color c = originalColors[i];
                sprites[i].color = new Color(c.r, c.g, c.b, alpha);
            }

            yield return null;
        }
    }



    IEnumerator FadeAndDisableFixToy()
    {
        audioSource.PlayOneShot(collectSound);
        yield return new WaitForSeconds(2f); // 2 saniye bekle

        SpriteRenderer[] sprites = fixToy.GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1f;
        float elapsed = 0f;

        // Orijinal renkleri sakla
        Color[] originalColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color;
        }

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            for (int i = 0; i < sprites.Length; i++)
            {
                Color c = originalColors[i];
                sprites[i].color = new Color(c.r, c.g, c.b, alpha);
            }

            yield return null;
        }

        fixToy.SetActive(false); // T�m sprite'lar solunca kapat

        O_SceneManager.Instance.WinGame(); // Oyun kazanma ekran�n� g�ster
    }

}
