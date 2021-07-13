using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController1 : MonoBehaviour
{
    public Animator animator;
    Vector3 vectorpos;
    Vector3 vectorscale;
    private double a;
    private int k;
    private float timer=1f;

    private void Awake()
    {
        vectorpos = gameObject.transform.position;
        vectorscale = gameObject.transform.localScale;  
        a = vectorpos.x;
        k = 0;
    }
    private void Update()
    {
        timer-= Time.deltaTime;
        
        if(k==0)
        {   
            vectorpos.x +=-0.05f;
            if(timer <= 0.0f)
            {
                k = 1;
                vectorscale.x *= -1;
                timer = 1f;
            }
        }
        else if(k==1)
        {
            vectorpos.x += 0.05f;
            if (timer <= 0.0f)
            {
                k = 0;
                vectorscale.x *= -1;
                timer = 1f;
            }

        }
        gameObject.transform.position = vectorpos;
        gameObject.transform.localScale = vectorscale;
        Debug.Log(timer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<PlayerController1>() != null)
        {
            animator.SetBool("Death", true);
            SceneManager.LoadScene(0);
        }
       
    }

}
