using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeController : MonoBehaviour
{
    

    public void StartShake()
    {
        cameraShake.instance.StartShake(2f, 6f);
    }

    public void EndShake()
    {
        cameraShake.instance.EndShake();
    }
}
