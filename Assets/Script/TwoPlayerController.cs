using UnityEngine;

public class TwoPlayerController : MonoBehaviour
{
    public GameObject player1; // Assign Player 1 in Inspector
    public GameObject player2; // Assign Player 2 in Inspector

    public float movementSpeed = 3f;
    private bool isFighting1 = false;
    private bool isFighting2 = false;

    private Rigidbody2D rb1, rb2;
    private Animator anim1, anim2; // Separate animators for each player

    private bool isFacingRight1 = true;
    private bool isFacingRight2 = true;

    private void Start()
    {
        // Get Rigidbody and Animator for both players
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();

        anim1 = player1.GetComponent<Animator>(); // Player 1's Animator
        anim2 = player2.GetComponent<Animator>(); // Player 2's Animator
    }

    private void Update()
    {
        float horizontalInput1 = Input.GetAxisRaw("Horizontal"); // Player 1 (Arrow Keys)
        float horizontalInput2 = 0;

        if (Input.GetKey("a")) horizontalInput2 = -1f;
        if (Input.GetKey("d")) horizontalInput2 = 1f;

        // Player 1 Movement
        if (!isFighting1)
        {
            rb1.linearVelocity = new Vector2(horizontalInput1 * movementSpeed, rb1.linearVelocity.y);
            anim1.SetFloat("xVelocity", Mathf.Abs(rb1.linearVelocity.x)); // Update Player 1 animation
        }

        // Player 2 Movement
        if (!isFighting2)
        {
            rb2.linearVelocity = new Vector2(horizontalInput2 * movementSpeed, rb2.linearVelocity.y);
            anim2.SetFloat("xVelocity", Mathf.Abs(rb2.linearVelocity.x)); // Update Player 2 animation
        }

        // Flip Player 1
        if ((horizontalInput1 > 0 && !isFacingRight1) || (horizontalInput1 < 0 && isFacingRight1))
        {
            Flip(ref isFacingRight1, player1);
        }

        // Flip Player 2
        if ((horizontalInput2 > 0 && !isFacingRight2) || (horizontalInput2 < 0 && isFacingRight2))
        {
            Flip(ref isFacingRight2, player2);
        }

        // Player 1 Fight (Arrow Up)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartFighting(ref isFighting1, anim1);
        }

        // Player 2 Fight (W Key)
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartFighting(ref isFighting2, anim2);
        }
    }

    private void Flip(ref bool isFacingRight, GameObject player)
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = player.transform.localScale;
        scale.x *= -1;
        player.transform.localScale = scale;
    }

    private void StartFighting(ref bool isFighting, Animator animator)
    {
        isFighting = true;
        animator.SetBool("isFighting", true);
        Invoke("StopFighting", 0.5f);
    }

    private void StopFighting()
    {
        isFighting1 = false;
        isFighting2 = false;
        anim1.SetBool("isFighting", false);
        anim2.SetBool("isFighting", false);
    }
}
