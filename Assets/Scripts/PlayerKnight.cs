using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnight : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float rotSpeed = 2.0f;
    [SerializeField] private float targetReactionRadius = 5.0f;
    [SerializeField] private float wallReactionRadius = 4.0f;

    void Update ()
    {
        // Spot player from trespassing walls
        if (Physics.Linecast(transform.position, targetTransform.position, out var hit))
        {
            if (hit.transform.CompareTag($"wall") && Vector3.Distance (transform.position, hit.transform.position) < wallReactionRadius)
            {
                Debug.Log("I don't want to hit that wall!");
                return;
            }
        }
        
        // Stop if reached destiny
        if (Vector3.Distance(transform.position, targetTransform.position) < targetReactionRadius) return;
        var tarPos = targetTransform.position;
        tarPos.y = transform.position.y;
        var dirRot = tarPos - transform.position;
        var tarRot = Quaternion.LookRotation(dirRot);
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }
}
