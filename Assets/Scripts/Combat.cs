using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private GameObject _target;
    private float _damage;
    private bool _isFollowing, _isAttacking;
    private void Update()
    {
        if (_isFollowing)
        {
            transform.LookAt(_target.transform.position);
            transform.Translate(Vector3.forward * (Time.deltaTime * 5.0f));
        }

        if (_isAttacking)
        {
            _target.GetComponent<PlayerKnight>().OnDamage(_damage);
        }
    }
    public void Follow(GameObject target)
    {
        _target = target;
        _isFollowing = true;
        _isAttacking = false;
    }
    public void Attack(GameObject target, float damage)
    {
        _target = target;
        _damage = damage;
        _isFollowing = false;
        _isAttacking = true;
    }

    public void Idle()
    {
        _isFollowing = false;
        _isAttacking = false;
    }
}
