using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You Win!");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}