using UnityEngine;
using System.Collections;

public class R_RandomSpawner : MonoBehaviour
{
    public GameObject prefab;              // �retilecek nesne
    public Transform spawnPoint;           // �retim konumu
    public int count = 5;                  // Toplam ka� tane �retilecek
    public float forceAmount = 5f;         // Kuvvet miktar�
    public float delay = 0.3f;             // Her �retim aras�ndaki bekleme s�resi

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

            yield return new WaitForSeconds(delay); // Belirtilen s�re kadar bekle
        }
    }
}