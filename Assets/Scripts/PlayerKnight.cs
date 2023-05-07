using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnight : Sense
{

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private float _health = 100.0f;
    public int FieldOfView = 45;
    public int ViewDistance = 100;
    private GameObject target;
    private Transform targetTransform;
    private Vector3 rayDirection;

    protected override void Initialize()
    {
        target = GameObject.FindGameObjectWithTag("target");
        targetTransform = GameObject.FindGameObjectWithTag("target").transform;
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
        rayDirection = (targetTransform.position - transform.position).normalized;
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
        // Target
        if (aspect.aspectName == targetAfiliation)
        {
            // Follow
            GetComponent<Wander>().enabled = false;
            GetComponent<Combat>().Follow(target);
        }
        else
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Combat>().Idle();
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isEditor || targetTransform == null) return;
        var position = transform.position;
        Debug.DrawLine(position, targetTransform.position, Color.red);
        var frontRayPoint = position + (transform.forward * ViewDistance);
        var leftRayPoint = Quaternion.Euler(0f, FieldOfView * 0.5f, 0f) * frontRayPoint;
        var rightRayPoint = Quaternion.Euler(0f, -FieldOfView * 0.5f, 0f) * frontRayPoint;
        Debug.DrawLine(position, frontRayPoint, Color.green);
        Debug.DrawLine(position, leftRayPoint, Color.green);
        Debug.DrawLine(position, rightRayPoint, Color.green);
    }

    public void OnDamage(float damage)
    {
        _health -= damage;
        if (!(_health <= 0)) return;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        var aspect = other.GetComponent<Aspect>();
        if (aspect != null && aspect.aspectName == Aspect.AspectName.Obstacle)
        {
            GetComponent<Wander>().enabled = true;
            GetComponent<Combat>().Idle();
            GetComponent<Wander>().GetNextPosition();
        }
    }
}
