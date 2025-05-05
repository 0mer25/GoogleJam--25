using UnityEngine;

public class E_ChangeSprite : MonoBehaviour
{
    public Sprite openSprite;         // Açýk hali
    public Sprite closedSprite;       // Kapalý hali
    public GameObject relatedObject;  // Ýlgili nesne (Inspector'dan atanacak)

    private SpriteRenderer spriteRenderer;
    private bool isOpen = false;

    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedSprite;
    }

    void OnMouseDown()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            spriteRenderer.sprite = openSprite;
            audioSource.PlayOneShot(openSound);

            if (relatedObject != null)
            {
                SpriteRenderer relatedRenderer = relatedObject.GetComponent<SpriteRenderer>();
                if (relatedRenderer != null)
                {
                    relatedRenderer.sortingOrder = 1;
                    if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable c)) c.isCollectable = true;

                }
            }
        }
        else
        {
            spriteRenderer.sprite = closedSprite;
            audioSource.PlayOneShot(closeSound);

            if (relatedObject != null)
            {
                SpriteRenderer relatedRenderer = relatedObject.GetComponent<SpriteRenderer>();
                if (relatedRenderer != null)
                {
                    relatedRenderer.sortingOrder = -1;
                    if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable a)) a.isCollectable = false;
                }
            }
        }
    }

}
