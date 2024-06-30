using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

[RequireComponent(typeof(SpriteAnimator))]
public class NPCController : MonoBehaviour{
    [SerializeField] private SpriteAnimator _spriteAnimator;
    private bool _isAlive = true;

    [SerializeField] private Transform _patrolStartPoint;
    [SerializeField] private Transform _patrolEndPoint;
    private float _duration = 2f;
    private bool _isForward = true;
    private float _moveSpeed = 1f;

    private void Start(){
        _spriteAnimator.StartAnimation();
        if (_isForward){
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void Update(){
        if (!_isAlive){
            return;
        }
        var targetPoint = _isForward?_patrolEndPoint : _patrolStartPoint;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, _moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f){
            _isForward = !_isForward;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }
    }

    private void OnDead(){
        _isAlive = false;
        _spriteAnimator.StopAnimation();
    }
}