using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicMode : MonoBehaviour
{
    public static cinematicMode instance;


    public bool cinematicModeOn;
    public Animator anim;

    void start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!cinematicModeOn)
            {
                CinematicModeOn();
                cinematicModeOn = true;
            }
            else
            {
                CinematicModeOff();
                cinematicModeOn = false;
            }
            
        }
    }

    public void CinematicModeOn()
    {
       anim.SetTrigger("cinematicOn");
    }

    public void CinematicModeOff()
    {
        anim.SetTrigger("cinematicOff");
    }

}
