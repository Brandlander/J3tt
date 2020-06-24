using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    Animator anim;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float updraftHeight;
    [SerializeField] float dashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(moveHorizontal * walkSpeed, moveVertical * walkSpeed);

        Move(direction);

        // Normal Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0f, jumpHeight);
            print("Jump");
        }

        // Updraft
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rb.velocity = new Vector2(0f, updraftHeight);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity = new Vector2(dashSpeed * direction.x, 0f);
        }


        print("Move:" + direction);

        anim.SetFloat("Speed", moveHorizontal);
    }

    void Move(Vector2 direction)
    {
        transform.position += transform.right * direction.x * Time.deltaTime;
            //+ transform.up * direction.y * Time.deltaTime;
    }
}
