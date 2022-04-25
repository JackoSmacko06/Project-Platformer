using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{

    public Transform portal2Pos;

    public bool up;
    public float upForce;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            
            if(PlayerController.instance.canTP == true)
            {
                collision.transform.position = portal2Pos.position;

                PlayerController.instance.extraJumps = PlayerController.instance.extraJumpsValue;

                if (up)
                {
                    PlayerController.instance.PortalJump(-upForce);
                }

                PlayerController.instance.canTP = false;
                Invoke("canTPTrue", .5f);
            }
            
        }
    }
    void canTPTrue()
    {
        PlayerController.instance.canTP = true;
    }
}
