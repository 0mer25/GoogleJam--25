using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Paddle : MonoBehaviour
{
    public GameManager gameManager;
    float leftScreenEdge = -4.77f;
    float rightScreenEdge = 4.77f;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.arcadeGameOver)
        {
            this.speed = 0;
        }
        PaddleMovemenetKeyboard();
    }

    void PaddleMovemenetKeyboard()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * speed);

        if (transform.position.x < leftScreenEdge)
        {
            transform.position = new Vector2(leftScreenEdge, transform.position.y);
        }

        if (transform.position.x > rightScreenEdge)
        {
            transform.position = new Vector2(rightScreenEdge, transform.position.y);
        }
    }
}
