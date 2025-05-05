using UnityEngine;
using System.Collections;

public class R_RandomSpawner : MonoBehaviour
{
    public GameObject prefab;              // Üretilecek nesne
    public Transform spawnPoint;           // Üretim konumu
    public int count = 5;                  // Toplam kaç tane üretilecek
    public float forceAmount = 5f;         // Kuvvet miktarý
    public float delay = 0.3f;             // Her üretim arasýndaki bekleme süresi

    void Start()
    {
        StartCoroutine(SpawnObjectsSequentially());
    }

    IEnumerator SpawnObjectsSequentially()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDir * forceAmount, ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(delay); // Belirtilen süre kadar bekle
        }
    }
}