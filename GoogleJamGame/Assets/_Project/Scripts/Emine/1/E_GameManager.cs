using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GameManager : MonoBehaviour
{
    public int score;
    public int fixScore;
    public bool scoreComlpleted = false;
    public bool fixScoreCompleted = false;

    // Update is called once per frame
    void Update()
    {
        if (score == 6)
        {
            scoreComlpleted = true;
           
        }

        if (fixScore == 6)
        {
            fixScoreCompleted = true;

        }
    }
}
