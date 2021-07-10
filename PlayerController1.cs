using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{
    [Header("animator")]
    public Animator animator;

    public float speed;
    private Rigidbody2D rb2d;
    public float jump;
    public BoxCollider2D boxCollider2D;

    [Header("Crouch")]
    public float crouchoffsetx;
    public float crouchoffsety;
    public float crouchsizex;
    public float crouchsizey;
    public float offsetx;
    public float offsety;
    public float sizex;
    public float sizey;
    private float vertical;
    private float horizontal;
    public LaserController laserController;
    public string scene;
    public Image[] healthimage;
    private int k;
    private readonly float t = 3f;
    private int flag;
  


    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        k = 2;
        flag = 1;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        AnimatePlayer(horizontal);
        MovePlayer(horizontal);
        Downfall(transform.position);
        
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.GetComponent<EnemyController1>() != null) || (collision.gameObject.GetComponent<EnemyController2>() != null))
        {
            Debug.Log("Hurt");

            animator.Play("Hurt");

            healthimage[k].enabled = false;

            if (k == 0)
            {
                if(flag==1)
                {
                    Debug.Log("Death");
                    animator.SetBool("Death", true);
                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                   flag = 0;
                }
                StartCoroutine(Reloadscene());
            }
            else
            {   
                k--;
            }
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
            animator.SetBool("Death", true);
            StartCoroutine(Reloadscene());
        }
    }


    void AnimatePlayer(float horizontal)
    {
        animator.SetBool("Hurt", false);
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("Attack", true);

        }
        else
        {
            animator.SetBool("Attack", false);
            laserController.gameObject.SetActive(false);
        }

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
        if (vertical>0)
        {
            animator.SetBool("Jump", true);
            rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Force);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        
    }

   

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("Jump", false);
        //animator.SetBool("Hurt", false);
        if ((collision.gameObject.GetComponent<EnemyController1>() != null) || (collision.gameObject.GetComponent<EnemyController2>() != null))
        {
            animator.Play("IdleWithGun");
        }
    }

    private void EnableLaser()
    {
        laserController.gameObject.SetActive(true);
    }
}
