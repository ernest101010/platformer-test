using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay{
    public class SpriteAnimator: MonoBehaviour{
        
        [SerializeField] private List<Sprite> _animationSprites = new ();
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _frameDelay = 0.05f;
        [SerializeField] private bool _playOnAwake;

        private int _animationSpriteIndex;
        private bool _isPlayed;

        private void Start(){
            if (_playOnAwake){
                StartAnimation();
            }
        }


        public void StartAnimation(){
            _isPlayed = true;
            StartCoroutine(Animate());
        }

        public void StopAnimation(){
            _isPlayed = false;
        }
        private IEnumerator Animate(){
            while (_isPlayed){
                if (_animationSpriteIndex ==_animationSprites.Count ){
                    _animationSpriteIndex = 0;
                }
                _spriteRenderer.sprite = _animationSprites[_animationSpriteIndex];
                yield return new WaitForSeconds(_frameDelay);
                _animationSpriteIndex++;
            }
        }
    }
}