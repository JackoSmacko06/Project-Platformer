using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeInAnim : MonoBehaviour
{
    
    public void StartShake()
    {

        cameraShake.instance.StartShake(1f, 3f);
        //PlayerController.instance.Freeze();

    }

    public void EndShake()
    {
        cameraShake.instance.EndShake();
        //PlayerController.instance.FreezeStop();


    }
}
