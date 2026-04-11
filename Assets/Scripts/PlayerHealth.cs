using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;
    private int currentHealth;
    public HealthBar healthBar;

    [Header("Invincibility")]
    public float invincibleTime = 1f;
    private bool isInvincible = false;

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log("Player HP: " + currentHealth);

        // Hurt animatie
        animator.SetTrigger("Hurt");
        StartCoroutine(ResetHurtTrigger(0.2f));

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    IEnumerator ResetHurtTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.ResetTrigger("Hurt");
    }

    void Die()
    {
        Debug.Log("Player died");

        GetComponent<PlayerMovement>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("Die");

        StartCoroutine(WaitAndReloadScene(1f));
    }

    IEnumerator WaitAndReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}