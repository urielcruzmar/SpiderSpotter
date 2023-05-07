using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    private void OnTriggerEnter(Collider other)
    {
        var aspect = other.GetComponent<Aspect>();
        if (aspect != null && aspect.aspectName == targetAfiliation)
        {
            GetComponent<Combat>().Attack(other.gameObject, 10.0f);
        }
        // Obstacle
        else if (aspect.aspectName == Aspect.AspectName.Obstacle)
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Combat>().Idle();
            GetComponent<Wander>().GetNextPosition();
        }
    }
}
