using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController1 : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private ScoreController1 scr;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController1>() != null)
        {
            Destroy(gameObject);
            scr.Updatescore();
            audioSource.Play();
        }
    }
}
