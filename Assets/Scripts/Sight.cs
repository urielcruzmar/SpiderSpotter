using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : Sense
{
    public int FieldOfView = 45;
    public int ViewDistance = 100;
    private GameObject player;
    private Transform playerTransform;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense()
    {
        OnDrawGizmos();
        ElapsedTime += Time.deltaTime;
        if (!(ElapsedTime >= detectionRate)) return;
        DetectAspect();
        ElapsedTime = 0.0f;
    }

    private void DetectAspect()
    {
        rayDirection = (playerTransform.position - transform.position).normalized;
        // Check FOV and Aspect
        if (!(Vector3.Angle(rayDirection, transform.forward) < FieldOfView) ||
            !Physics.Raycast(transform.position, rayDirection, out var hit, ViewDistance)) return;
        var aspect = hit.collider.GetComponent<Aspect>();
        if (aspect == null)
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Combat>().Idle();
            return;
        }
        // Enemy
        if (aspect.aspectName == targetAfiliation)
        {
            // Follow
            GetComponent<Wander>().enabled = false;
            GetComponent<Combat>().Follow(player);
        }
        else
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Combat>().Idle();
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isEditor || playerTransform == null) return;
        var position = transform.position;
        Debug.DrawLine(position, playerTransform.position, Color.red);
        var frontRayPoint = position + (transform.forward * ViewDistance);
        var leftRayPoint = Quaternion.Euler(0f, FieldOfView * 0.5f, 0f) * frontRayPoint;
        var rightRayPoint = Quaternion.Euler(0f, -FieldOfView * 0.5f, 0f) * frontRayPoint;
        Debug.DrawLine(position, frontRayPoint, Color.green);
        Debug.DrawLine(position, leftRayPoint, Color.green);
        Debug.DrawLine(position, rightRayPoint, Color.green);
    }
}
