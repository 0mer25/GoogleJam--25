using UnityEngine;

public class E_RotateOnClick : MonoBehaviour
{
    private float rotateAmount = 60f;      // Kaç derece dönecek
    private float rotateDuration = .5f;   // Ne kadar sürede dönecek
    public bool rotateRight = true;       // Saða mý sola mý dönsün

    private bool isRotating = false;
    public GameObject relatedObject;

    public AudioSource audioSource;
    public AudioClip openSound;

    private void OnMouseDown()
    {
        if (!isRotating)
            StartCoroutine(RotateObject());
    }

    private System.Collections.IEnumerator RotateObject()
    {
        isRotating = true;
        audioSource.PlayOneShot(openSound);
        float elapsed = 0f;
        Quaternion startRotation = transform.rotation;
        float direction = rotateRight ? 1f : -1f;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, direction * rotateAmount);

        // Ýlk dönüþ
        while (elapsed < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsed / rotateDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
        if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable c)) c.isCollectable = true;


        yield return new WaitForSeconds(3f);
        if (relatedObject && relatedObject.TryGetComponent(out E_IsCollectable a)) a.isCollectable = false;

        audioSource.PlayOneShot(openSound);
        // Geri dönüþ
        elapsed = 0f;
        while (elapsed < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(targetRotation, startRotation, elapsed / rotateDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = startRotation;

        isRotating = false;
    }
}
