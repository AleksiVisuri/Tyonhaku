using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField]
    private KeyCode Left;

    [SerializeField]
    private KeyCode Right;

    [SerializeField]
    private KeyCode Jump;

    [SerializeField]
    private KeyCode Jump2;

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private float JumpForce;

    private Rigidbody2D rb2d;

    private SpriteRenderer SpriteRenderer;

    public int direction;

    private int JumpCounter;

    private bool IsGrounded;

    [SerializeField]
    private Transform GroundCheck;

    [SerializeField]

    private LayerMask GroundLayer;

    public bool GroundCheckRadius;


    private Animator Animations;

    private float Speed1;




    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        Animations = GetComponent<Animator>();
    }


    void Update()
    {

        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, GroundLayer);

        MovePlayer();

        Animation();

    }

    private void MovePlayer()
    {
        if (Input.GetKey(Left))
        {
            rb2d.velocity = new Vector2(-MoveSpeed, rb2d.velocity.y);

            direction = -1;

            SpriteRenderer.flipX = true;

        }
        else if (Input.GetKey(Right))

        {
            rb2d.velocity = new Vector2(MoveSpeed, rb2d.velocity.y);

            direction = 1;

            SpriteRenderer.flipX = false;



        }
        else if (rb2d.velocity.y != 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y);
        }
        else
        {

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (Input.GetKeyDown(Jump) || (Input.GetKeyDown(Jump2)))
        {

            if (JumpCounter < 2)
            {



                rb2d.velocity = new Vector2(rb2d.velocity.x, JumpForce);

                JumpCounter++;

            }
        }

        if (IsGrounded)
        {

            JumpCounter = 1;

        }


    }

    private void Animation()
    {
        

        Animations.SetBool("IsGround", IsGrounded);

        Animations.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Enemy2"))
        {

            Destroy(gameObject);




        }


    }
}


