using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 1;
    public float knockbackForce = 5f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerMovement movement = collision.gameObject.GetComponent<PlayerMovement>();

        if (movement != null && movement.isParrying)
        {
            return;
        }

        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if (player != null)
        {
            player.TakeDamage(damage);

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockback = new Vector2(
                    collision.transform.position.x < transform.position.x ? -knockbackForce : knockbackForce,
                    knockbackForce / 2
                );

                rb.linearVelocity = knockback;
            }
        }
    }
}