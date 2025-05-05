using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class R_TicketManager : MonoBehaviour
{
    public int ticketCount = 0;
    public Text ticketText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ticketText.text = "Ticket: " + ticketCount.ToString();
    }
}
