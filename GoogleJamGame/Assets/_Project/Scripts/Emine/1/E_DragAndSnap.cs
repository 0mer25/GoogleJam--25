using DG.Tweening.Core.Easing;
using UnityEngine;

public class E_DragAndSnap : MonoBehaviour
{
    private Vector3 startPos;
    private bool isDragging = false;
    private Vector3 offset;

    public string correctTargetTag; // Doðru yerin tag'i
    private bool isPlaced = false;

    private Collider2D myCollider;
    private E_GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip collectSound;

    void Start()
    {
        startPos = transform.position;
        myCollider = GetComponent<Collider2D>();
        gameManager = GameObject.FindObjectOfType<E_GameManager>();
    }

    void OnMouseDown()
    {
        if (isPlaced) return;

        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (isPlaced) return;

        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        if (isPlaced) return;

        isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);

        foreach (var otherCol in colliders)
        {
            if (otherCol.CompareTag(correctTargetTag))
            {
                audioSource.PlayOneShot(collectSound);
                // Doðru yere yerleþti
                transform.position = otherCol.transform.position;
                isPlaced = true;
                gameManager.fixScore++;
                // Artýk tekrar týklanamasýn diye collider'ý kapat
                if (myCollider != null) myCollider.enabled = false;

                return;
            }
        }

        // Yanlýþ yere býrakýldýysa geri dön
        transform.position = startPos;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
