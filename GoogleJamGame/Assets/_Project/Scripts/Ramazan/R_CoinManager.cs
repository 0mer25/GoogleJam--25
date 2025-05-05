using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class R_CoinManager : MonoBehaviour
{
    [SerializeField] public int coinCount = 0;
    public Text coinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = "Coin: " + coinCount.ToString();
    }
    public bool UseCoin()
    {
        if (coinCount > 4)
        {
            coinCount-=4;
            return true;
        }
        return false;
    }
}
