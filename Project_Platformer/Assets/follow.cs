using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{

    public GameObject target;

    void Update()
    {
        transform.position = target.transform.position;
    }
}
