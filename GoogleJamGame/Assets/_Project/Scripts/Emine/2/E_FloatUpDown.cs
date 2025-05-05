using UnityEngine;

public class E_FloatUpDown : MonoBehaviour
{
    public float amplitude = 0.5f;  // Yukarý-aþaðý ne kadar gidecek (dünya biriminde)
    public float speed = 2f;        // Hýzý
    public bool isMovementPaused = false;  // Hareket durdurulmuþ mu?

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;  // Nesnenin baþlangýç konumu
    }

    void Update()
    {
        if (isMovementPaused)
        {          
            return;  // Hareketi durdur
        }

        // Eðer hareket durdurulmadýysa, devam et
        float newY = Mathf.PingPong(Time.time * speed, amplitude * 2) - amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + newY, startPos.z);
    }

}
