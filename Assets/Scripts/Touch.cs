using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    private void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null && aspect.aspectName == targetAfiliation)
        {
            Debug.Log("Enemy touched!");
        }
    }
}
