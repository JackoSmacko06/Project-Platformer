using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextlvl : MonoBehaviour
{
    public bool black;
    public bool white;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (black)
            {
                levelLoader.Instance.nextLevelBlack();
            }
            else if (white)
            {
                levelLoader.Instance.nextlevelWhite();
            }
        }
    }
}
