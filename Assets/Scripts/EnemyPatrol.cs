using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftPoint;
    public Transform rightPoint;

    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                rightPoint.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, rightPoint.position) < 0.01f)
                movingRight = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                leftPoint.position,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, leftPoint.position) < 0.01f)
                movingRight = true;
        }

        // flip sprite
        spriteRenderer.flipX = !movingRight;
    }
}