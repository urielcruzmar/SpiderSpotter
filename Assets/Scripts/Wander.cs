using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    private Vector3 targetPosition;

    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 2.0f;
    [SerializeField] private float minX = -45.0f;
    [SerializeField] private float maxX = 45.0f;
    [SerializeField] private float minZ = 45.0f;
    [SerializeField] private float maxZ = -45.0f;
    [SerializeField] private float targetReactionRadius = 5.0f;
    [SerializeField] private float targetVerticalOffset = 0.5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        GetNextPosition();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!(Vector3.Distance(targetPosition, transform.position) <= targetReactionRadius)) return;
        GetNextPosition();
        // Rotate and translate
        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(new Vector3(0f, 0f, movementSpeed * Time.deltaTime));
    }
    
    private void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), targetVerticalOffset, Random.Range(minZ, maxZ));
    }
}
