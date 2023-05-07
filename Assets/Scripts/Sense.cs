using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    public bool bDebug = true;

    public Aspect.AspectName targetAfiliation = Aspect.AspectName.Player;

    public float detectionRate = 1.0f;

    protected float ElapsedTime = 0.0f;

    protected virtual void Initialize(){ }

    protected virtual void UpdateSense(){ }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSense();
    }
}
