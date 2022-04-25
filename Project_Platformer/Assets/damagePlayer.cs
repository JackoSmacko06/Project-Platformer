using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    public int damage;

    public bool hazard;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!hazard)
            {
                Health.instance.TakeDamage(damage);
            }
            else
            {
                Health.instance.TakeDamageSpikes(damage);
            }
            
        }
    }
}
