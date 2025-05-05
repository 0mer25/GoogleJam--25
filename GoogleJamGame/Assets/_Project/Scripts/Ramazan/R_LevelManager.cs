using PlayerMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class R_LevelManager : MonoBehaviour
{
    public R_TicketManager ticketManager;
    private bool playerInRange = false;
    private bool playerInStoreRange = false;
    public int requiredTickets = 10;
    public R_CoinManager coinManager;
    public R_PlayerMovement playerMovement;
    //private bool rewardStarted = false;



    // Start is called before the first frame update
    void Start()
    {
        ticketManager = FindObjectOfType<R_TicketManager>();
        coinManager = FindObjectOfType<R_CoinManager>();
        playerMovement = FindObjectOfType<R_PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && coinManager != null && coinManager.UseCoin())
        {
            Debug.Log("E tuþuna basýldý, sahne deðiþtiriliyor...");
            Debug.Log("coinManager: " + coinManager.coinCount);
            SceneManager.LoadScene("R_ArcadeMachine");
        }
        if (playerInStoreRange && Input.GetKeyDown(KeyCode.E))
        {
            if (ticketManager.ticketCount >= requiredTickets)
            {
                //rewardStarted = true;
                //Debug.Log("Yeterli ticket varrrrrrrr.");
                SceneManager.LoadScene("R_AwardScene");
            }
            else
            {
                //Debug.Log("Yeterli ticket yok.");
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (SceneManager.GetActiveScene().name == ("R_AwardScene"))
            {
                //SceneManager.LoadScene("R_HowTo");
                O_SceneManager.Instance.WinGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arcademachine"))
        {
            playerInRange = true;
            Debug.Log("Oyuncu geçiþ alanýna girdi.");
            Debug.Log("coinManager: " + coinManager.coinCount);
            //Debug.Log("ticketManager: " + ticketManager.ticketCount);
            //Debug.Log("playerMovement: " + playerMovement.ticketManager.ticketCount);
        }
        else if (other.CompareTag("NPCCashier"))
        {
            playerInStoreRange = true;
            Debug.Log("Oyuncu NPC geçiþ alanýna girdi.");
            CheckTicketAmount();
            //Debug.Log("ticketManager: " + ticketManager.ticketCount);
            //Debug.Log("playerMovement: " + playerMovement.ticketManager.ticketCount);
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("arcademachine"))
        {
            playerInRange = false;
            Debug.Log("Oyuncu geçiþ alanýndan çýktý.");
        }
        if (other.CompareTag("NPCCashier"))
        {
            playerInStoreRange = false;
            Debug.Log("Oyuncu NPC geçiþ alanýndan çýktý.");
            CheckTicketAmount();
        }
    }
    public void GameOver()
    {
        Debug.Log("Game over level manager test");
        SceneManager.LoadScene("R_ArcadeMachineGameOver");
        GameManager.score = 0;
        GameManager.lives = 3;
    }

    public void CheckTicketAmount()
    {
        if (ticketManager == null)
        {
            Debug.LogError("ticketManager null, sahnede baðlanmamýþ!");
            return;
        }
        if (ticketManager.ticketCount > 10)
        {
            Debug.Log("Ticket sayisi: " + ticketManager.ticketCount);
            //SceneManager.LoadScene("R_AwardScene");
        }
        else
        {
            Debug.Log("Yeterli ticket yok: " + ticketManager.ticketCount);
        }
    }
}
