using UnityEngine;

public class E_FloatUpDown : MonoBehaviour
{
    public float amplitude = 0.5f;  // Yukar�-a�a�� ne kadar gidecek (d�nya biriminde)
    public float speed = 2f;        // H�z�
    public bool isMovementPaused = false;  // Hareket durdurulmu� mu?

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;  // Nesnenin ba�lang�� konumu
    }

    void Update()
    {
        if (isMovementPaused)
        {          
            return;  // Hareketi durdur
        }

        // E�er hareket durdurulmad�ysa, devam et
        float newY = Mathf.PingPong(Time.time * speed, amplitude * 2) - amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }

}
