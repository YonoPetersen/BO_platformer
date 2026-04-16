using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value = 1;
    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");

            CoinManager.instance.AddCoin(value);
            Destroy(gameObject);

            if (coinSound != null)
                {
                    AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
        }
    }
}