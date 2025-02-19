using UnityEngine;

public class sabrinacode : MonoBehaviour
{
    public float movementSpeed = 3f;
    private bool isFacingRight = true;
    private bool isFighting = false;
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


        if (!isFighting)
        {
            rb.linearVelocity = new Vector2(horizontalInput * movementSpeed, rb.linearVelocity.y);
            animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        }

        // Flip character if moving in the opposite direction
        if ((horizontalInput > 0 && !isFacingRight) || (horizontalInput < 0 && isFacingRight))
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartFighting();
        }
    }

    private void StartFighting()
    {
        isFighting = true;
        animator.SetBool("isFighting", true);

        Invoke("StopFighting", 1f);
    }

    private void StopFighting()
    {
        isFighting = false;
        animator.SetBool("isFighting", false);
    }


    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
