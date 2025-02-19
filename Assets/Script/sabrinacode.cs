using UnityEngine;

public class sabrinacode : MonoBehaviour
{
    public float movementSpeed = 3f;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Apply movement using Rigidbody2D
        rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);

        // Update Blend Tree parameter (controls transition between idle and run animations)
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));

        // Flip character if moving in the opposite direction
        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
