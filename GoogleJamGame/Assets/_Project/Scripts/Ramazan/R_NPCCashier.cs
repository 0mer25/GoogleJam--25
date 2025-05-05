using UnityEngine;
using UnityEngine.SceneManagement;

public class R_NPCCashier : MonoBehaviour
{
    private bool playerInRange = false;
    public R_TicketManager ticketManager; // Inspector'dan ata!
    public int requiredTickets = 10;
    public string nextSceneName; // "OdulSahnesi" gibi
    void Start()
    {

    }
    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (ticketManager.ticketCount >= requiredTickets)
            {
                Debug.Log("Yeterli ticket! Ödül veriliyor.");
                SceneManager.LoadScene("R_AwardScene");
            }
            else
            {
                Debug.Log("Yeterli ticket yok.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
