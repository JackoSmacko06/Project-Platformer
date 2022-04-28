using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissapearingPlatform : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private SpriteRenderer spr;

    public float waitTime;
    public float regenTime;

    private void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
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
        boxCol.enabled = false;
        spr.enabled = false;
        yield return new WaitForSeconds(regenTime);
        boxCol.enabled = true;
        spr.enabled = true;

    }

}