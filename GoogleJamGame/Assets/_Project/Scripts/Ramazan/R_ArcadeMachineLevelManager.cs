using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class R_ArcadeMachineLevelManager : MonoBehaviour
{
    public R_CoinManager coinManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (SceneManager.GetActiveScene().name == ("R_ArcadeMachineWin"))
            {
                SceneManager.LoadScene("R_ArcadeSaloonAfterGame");
            }
            else if (SceneManager.GetActiveScene().name == ("R_ArcadeMachine"))
            {
                SceneManager.LoadScene("R_BrickGame_L1");
            }
            else if (coinManager != null && coinManager.UseCoin())
            {
                SceneManager.LoadScene("R_BrickGame_L1");
            }
            else
            {
                Debug.Log("Yetersiz coin!");
            }
        }
    }
}
