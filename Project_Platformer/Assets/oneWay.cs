using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneWay : MonoBehaviour
{

    private PlatformEffector2D effector;

    bool collided;

    private BoxCollider2D boxcol;
    private PolygonCollider2D polcol;

    private void Start()
    {

        effector = GetComponent<PlatformEffector2D>();

        boxcol = GetComponent<BoxCollider2D>();
        polcol = GetComponent<PolygonCollider2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            collided = true;
            
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            collided = false;

        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (collided)
            {
                if(boxcol != null)
                {
                    StartCoroutine(OneWay(.4f));
                }
                else if(polcol != null)
                {
                    StartCoroutine(OneWayPoly(.4f));
                }

                
            }
            
            
        }
    }

    IEnumerator OneWay(float time)
    {
        //effector.rotationalOffset = 180;
        boxcol.enabled = false;
        yield return new WaitForSeconds(time);
        boxcol.enabled = true;
        //ffector.rotationalOffset = 0;
    }

    IEnumerator OneWayPoly(float time)
    {
        polcol.enabled = false;
        yield return new WaitForSeconds(time);
        polcol.enabled = true;
    }
}
