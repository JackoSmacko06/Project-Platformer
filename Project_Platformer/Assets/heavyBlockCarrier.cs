using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heavyBlockCarrier : MonoBehaviour
{


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "block")
        {
            collision.transform.SetParent(this.transform);
            //this.transform.SetParent(collision.transform);
        }

        if (PlayerController.instance.changingDirection == true)
        {
            collision.transform.SetParent(null);
        }
    }



}
