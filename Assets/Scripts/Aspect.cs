using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour
{
    public enum AspectName
    {
        Player,
        Enemy,
        Obstacle,
        Target
    }
    public AspectName aspectName;
}
