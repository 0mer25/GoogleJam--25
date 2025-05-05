using UnityEngine;
using UnityEngine.SceneManagement;

public class O_SceneManager : MonoBehaviour
{
    public static O_SceneManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private O_AniScriptable scriptable;


    public void WinGame()
    {
        scriptable.isCompleted = true;

        SceneManager.LoadScene(0);
    }
}
