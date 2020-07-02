using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator anim;
    TrailRenderer tr;

    [SerializeField] float walkSpeed;

    // Fall speeds
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
 
    [Range(1, 10)]
    public float jumpVelocity;
    [Range(1, 20)]
    public float upDraftVelocity;

    public float dashSpeed;
    public float dashRange;
    public float dashTime;

    bool isFalling;
    bool dashing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {       
        float moveHorizontal = Input.GetAxis("Horizontal");   

        Vector2 direction = new Vector2(moveHorizontal * walkSpeed, 0);

        Move(direction);

        // Gravity
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            anim.SetBool("Updraft", false);
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Normal Jump
        if (Input.GetKeyDown(KeyCode.Space) && !isFalling)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            print("Jump");
        }

        // Updraft
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = Vector2.up * upDraftVelocity;
            anim.SetBool("Updraft", true);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(direction.x < 0)
            {
                rb.velocity = Vector2.left * dashSpeed;
            } else if( direction.x > 0)
            {
                rb.velocity = Vector2.right * dashSpeed;
            } else
            {
                rb.velocity = Vector2.right * dashSpeed;
            }
            anim.SetBool("dash", true);
            tr.emitting = true;
            StartCoroutine(DashTime());
        }
        
        print("Falling:" + isFalling);

        anim.SetFloat("Speed", moveHorizontal);
    }

    void Move(Vector2 direction)
    {
        transform.position += transform.right * direction.x * Time.deltaTime
            + transform.up * direction.y * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        isFalling = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isFalling = true;
    }

    IEnumerator DashTime()
    {
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        anim.SetBool("dash", false);
        tr.emitting = false;
        print("Stop dash");
    }
}
