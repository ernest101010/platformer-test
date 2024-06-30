using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimator : MonoBehaviour{
    [SerializeField] private List<Sprite> _animationSprites = new();
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _animationSpriteIndex = 0;
    private bool _isAlive = true;

    private void Start(){
        StartCoroutine(Animate());
    }

    private IEnumerator Animate(){
        while (_isAlive){
            if (_animationSpriteIndex == _animationSprites.Count){
                _animationSpriteIndex = 0;
            }

            _spriteRenderer.sprite = _animationSprites[_animationSpriteIndex];
            yield return new WaitForSeconds(0.08f);
            _animationSpriteIndex++;
        }
    }
}