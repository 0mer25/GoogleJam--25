using System.Collections.Generic;
using UnityEngine;

public class O_AniTopu : MonoBehaviour
{
    [SerializeField] private List<GameObject> openObjects;
    private bool isOpen = false;
    [SerializeField] private int sceneIndex = 0;
    [SerializeField] private O_AniScriptable o_AniScriptable;
    [SerializeField] private Sprite originalSprite;
    [SerializeField] private Sprite yellowSprite;

    void OnEnable()
    {
        if(o_AniScriptable.isCompleted)
        {
            GetComponent<SpriteRenderer>().sprite = yellowSprite;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = originalSprite;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isOpen)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Clicked();
            }
        }
    }

    private void Clicked()
    {
        isOpen = true;
        foreach (GameObject obj in openObjects)
        {
            obj.SetActive(true);
        }
    }

    public void CloseAll()
    {
        foreach (GameObject obj in openObjects)
        {
            obj.SetActive(false);
        }
        isOpen = false;
    }

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
