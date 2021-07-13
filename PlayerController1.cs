using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    public string scene;
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
    public LaserController laserController;
    private readonly float fallMultiplier = 5.5f;
    public Image[] healthimage;
    private int k;
    private readonly float t = 3f;
    private int flag;
    private bool tomove;
    private bool isground;
    private bool jetpack;


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        k = 2;
        flag = 1;
        tomove = true;
        isground = true;
    }

    void Start()
    {
        
    }


    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        AnimatePlayer(horizontal);
        if(tomove==true)
        {
            MovePlayer(horizontal);
        }
        
        Downfall(transform.position);

        if (rb2d.velocity.y <= 3)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<EnemyController1>() != null) || (collision.gameObject.GetComponent<EnemyController2>() != null))
        {

            animator.Play("Hurt");

            healthimage[k].enabled = false;

            if (k == 0)
            {
                if (flag == 1)
                {
                    animator.Play("Death");
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                    flag = 0;
                }
                StartCoroutine(Reloadscene());
            }
            else
            {
                k--;
            }

            if (collision.gameObject.CompareTag("ground"))
            {
                isground = true;
            }
        }

        else if (collision.gameObject.GetComponent<portioncontroller1>() != null)
        {
            while (k <= 1)
            {
                k++;
                healthimage[k].enabled = true;
            }
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Reloadscene()
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(scene);
    }



    private void Downfall(Vector2 vector2)
    {
        if (vector2.y <= -48.7)
        {
            animator.Play("Death");
            StartCoroutine(Reloadscene());
        }
    }


    void AnimatePlayer(float horizontal)
    {
        if(isground==true)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));
        }
      
        if(Input.GetKey(KeyCode.X))
        {
            tomove = false;
            animator.SetBool("Attack", true);
            
        }
        else
        {
            tomove = true;
            animator.SetBool("Attack", false);
            laserController.gameObject.SetActive(false);
        }

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

        else if (Input.GetKeyUp(KeyCode.LeftControl))
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
            StartCoroutine(WaitJump());
        }
        else
        {
            animator.SetBool("Jump", false);
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isground = true;
        }
    }

    IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(0.1f);
        rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Force);
        animator.SetBool("Jump", false);
    }

    private void EnableLaser()
    {
        laserController.gameObject.SetActive(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<EnemyController1>() != null) || (collision.gameObject.GetComponent<EnemyController2>() != null))
        {
            animator.Play("IdleWithGun");
        }
        if (!(vertical > 0))
        {
            animator.SetBool("Jump", false);
        }
        if(collision.gameObject.CompareTag("ground"))
        {
            isground = false;
        }

    }
}
