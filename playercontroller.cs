using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playercontroller : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private Rigidbody2D rb2d;
    public float jump;
    private float top;
    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player awake");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        top = vertical;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        AnimatePlayer(horizontal);
        MovePlayer(horizontal);
    }
    

    void AnimatePlayer(float horizontal)
    {  
       
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);
        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        if(!(top>0))
        {
            animator.SetBool("Jump", false);
        }
    }


    void MovePlayer(float horizontal)
    {
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;
    }


    private void OnCollisionStay2D(Collision2D collision)
    {   
        if (top > 0)
        {
            animator.SetBool("Jump", true);
            rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Force);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        
    }
}
