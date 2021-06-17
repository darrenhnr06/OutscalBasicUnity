using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{
    public Animator animator;
    public float speed;
    private Rigidbody2D rb2d;
    public float jump;
    public BoxCollider2D boxCollider2D;
    public float crouchoffsetx;
    public float crouchoffsety;
    public float crouchsizex;
    public float crouchsizey;
    public float offsetx;
    public float offsety;
    public float sizex;
    public float sizey;
    private float vertical;


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Debug.Log("Player awake");
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        AnimatePlayer(horizontal);
        MovePlayer(horizontal);
        Downfall(transform.position);
    }

    private void Downfall(Vector2 vector2)
    {
        if(vector2.y<=-15)
        {
            SceneManager.LoadScene(3);
        }
    }


    void AnimatePlayer(float horizontal)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

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

        if (!(vertical > 0))
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", true);
             boxCollider2D.offset = new Vector2(crouchoffsetx, crouchoffsety);
             boxCollider2D.size = new Vector2(crouchsizex, crouchsizey);
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            animator.SetBool("Crouch", false);
            boxCollider2D.offset = new Vector2(offsetx, offsety);
            boxCollider2D.size = new Vector2(sizex, sizey);
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
        if (vertical > 0)
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
