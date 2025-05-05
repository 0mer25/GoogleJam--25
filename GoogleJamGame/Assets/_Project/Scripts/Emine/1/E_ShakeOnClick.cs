using System.Collections;
using UnityEngine;

public class E_ShakeOnClick : MonoBehaviour
{
    public float rotationAngle = 10f;
    public float rotationDuration = 0.1f;

    private bool isSwinging = false;
    private Quaternion originalRotation;

    public AudioSource audioSource;
    public AudioClip openSound;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (!isSwinging)
        {
            StartCoroutine(SwingEffect());
        }
    }

    IEnumerator SwingEffect()
    {
        isSwinging = true;
        audioSource.PlayOneShot(openSound);
        Quaternion rightRotation = Quaternion.Euler(0, 0, rotationAngle);
        Quaternion leftRotation = Quaternion.Euler(0, 0, -rotationAngle);

        // Saða dön
        yield return RotateOverTime(originalRotation, rightRotation, rotationDuration);

        // Orijinale dön
        yield return RotateOverTime(rightRotation, originalRotation, rotationDuration);

        // Sola dön
        yield return RotateOverTime(originalRotation, leftRotation, rotationDuration);

        // Orijinale dön
        yield return RotateOverTime(leftRotation, originalRotation, rotationDuration);

        isSwinging = false;
    }

    IEnumerator RotateOverTime(Quaternion from, Quaternion to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
    }
}
