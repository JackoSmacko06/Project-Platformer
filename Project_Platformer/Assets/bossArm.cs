using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossArm : MonoBehaviour
{
    public ParticleSystem rumble;
    public GameObject black;
    
    void Start()
    {
       rumble.Stop();
        black.SetActive(false);
    }

    void Update()
    {
        
    }

    public void rumbleStart()
    {
        rumble.Play();
    }

    public void rumbleEnd()
    {
        rumble.Stop();
    }

    public void end()
    {
        levelLoader.Instance.nextLevelBlack();
    }

    public void Black()
    {
        black.SetActive(true);
    }
}
