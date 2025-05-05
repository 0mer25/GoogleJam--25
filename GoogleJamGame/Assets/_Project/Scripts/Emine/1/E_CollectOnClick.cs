using UnityEngine;

public class E_CollectOnClick : MonoBehaviour
{
    private E_GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip collectSound;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<E_GameManager>();
    }

    void OnMouseDown()
    {
        if (transform.GetComponent<E_IsCollectable>().isCollectable)
        {
            audioSource.PlayOneShot(collectSound);
            Destroy(gameObject);
            gameManager.score++;
        }
    }
}
