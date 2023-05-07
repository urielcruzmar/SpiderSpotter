using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float hOffset = 0.2f;

    private void Update()
    {
        var button = 0;
        if (!Input.GetMouseButtonDown(button)) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray.origin, ray.direction, out var hitInfo)) return;
        var targetPosition = hitInfo.point;
        transform.position = targetPosition + new Vector3(0.0f, hOffset, 0.0f);
    }
}
