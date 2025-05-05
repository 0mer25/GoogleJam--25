using UnityEngine;
using System.Collections;

public class E_SlideOnClick : MonoBehaviour
{
    public Vector3 moveOffset = new Vector3(1f, 0f, 0f); // Hangi yöne kayacaðýný ayarla
    public float moveDuration = 0.5f; // Ne kadar sürede kayacaðýný ayarla
    public float waitTime = 3f; // Bekleme süresi

    private bool isMoving = false;
    public GameObject relatedObject;

    public AudioSource audioSource;
    public AudioClip openSound;

    private void OnMouseDown()
    {
        if (!isMoving)
            StartCoroutine(SlideSequence());
    }

    private IEnumerator SlideSequence()
    {
        isMoving = true;
        audioSource.PlayOneShot(openSound);
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + moveOffset;

        // Kayma hareketi
        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
        if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable c)) c.isCollectable = true;
        // Bekleme süresi
        yield return new WaitForSeconds(waitTime);
        if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable a)) a.isCollectable = false;
        // Geri kayma
        audioSource.PlayOneShot(openSound);
        elapsed = 0f;
        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = startPosition;

        isMoving = false;
    }
}
