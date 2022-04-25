using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secret : MonoBehaviour
{

    private SpriteRenderer spr;

    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            spr.enabled = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            spr.enabled = true;

        }
    }
}
