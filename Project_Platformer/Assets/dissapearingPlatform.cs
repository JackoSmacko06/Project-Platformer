using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissapearingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private SpriteRenderer spr;

    public float waitTime;
    public float regenTime;
    private Animator anim;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            StartCoroutine(Dissapear());
            collision.gameObject.GetComponent<PlayerController>().coyoteTimeCounter = collision.gameObject.GetComponent<PlayerController>().coyoteTime;
        }
    }

    IEnumerator Dissapear()
    {

        yield return new WaitForSeconds(waitTime);
        anim.SetTrigger("fadeaway");
        //boxCol.enabled = false;
        //spr.enabled = false;
        yield return new WaitForSeconds(regenTime);
        anim.SetTrigger("fadeback");
        //boxCol.enabled = true;
        //spr.enabled = true;

    }

}