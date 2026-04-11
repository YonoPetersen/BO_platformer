using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");

            Destroy(gameObject);
        }
    }
}