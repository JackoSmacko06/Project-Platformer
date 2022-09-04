using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secrett : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("found");
        }
    }
}
