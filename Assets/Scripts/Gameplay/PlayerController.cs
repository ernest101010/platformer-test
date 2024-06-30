using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Gameplay{
    public class PlayerController : MonoBehaviour{
        [SerializeField] private PlayerAnimator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _jumpForce = 7f;

        [SerializeField] private Transform _spawnPoint;
        private bool _isMovementEnabled;
        private bool _isOnGround;
        private int _lifeCount = 3;
        private int _direction = 1;

        public event Action<int> UpdateLife;
        public event Action<int> GetPoint;
        public event Action LevelCompleted;

        public void ResetPlayer(){
            _lifeCount = 3;
            UpdateLife?.Invoke(_lifeCount);
            ReSpawn();
        }

        public void EnableMovement(bool enable){
            _isMovementEnabled = enable;
        }

        private void Update(){
            if (_isMovementEnabled){
                var xAxis = Input.GetAxis("Horizontal");
                if (_isOnGround){
                    if (xAxis != 0){
                        var direction = xAxis < 0 ? -1 : 1;
                        CheckDirection(direction);
                        _animator.SetState(PlayerAnimationState.Run);
                    }
                    else{
                        _animator.SetState(PlayerAnimationState.Idle);
                    }
                }

                transform.position += new Vector3(xAxis * _moveSpeed * Time.deltaTime, 0, 0);


                if (Input.GetKeyDown(KeyCode.Space) && _isOnGround){
                    _isOnGround = false;
                    _animator.SetState(PlayerAnimationState.Jump);
                    _rigidbody.velocity = Vector2.up * _jumpForce;
                }
            }
        }

        private void CheckDirection(int newDirection){
            if (_direction == newDirection){
                return;
            }
            
            _direction = newDirection;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
        }

        private void OnCollisionEnter2D(Collision2D collision){
            if (collision.gameObject.CompareTag("Ground")){
                _isOnGround = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other){
            if (other.gameObject.CompareTag("DeathZone") || other.gameObject.CompareTag("Enemy")){
                PlayerDead();
            }
            
            if (other.gameObject.CompareTag("Finish")){
                CompleteLevel();
            }

            if (other.gameObject.CompareTag("Coin")){
                GetPoint?.Invoke(1);
                other.gameObject.transform.DOScale(0, 0.1f).OnComplete(() => { Destroy(other.gameObject); });
            }
        }



        private void PlayerDead(){
            _isMovementEnabled = false;
            _lifeCount--;
            _animator.SetState(PlayerAnimationState.Dead, () => {
                if (_lifeCount > 0){
                    StartCoroutine(WaitAndRespawn());
                }
            });
            UpdateLife?.Invoke(_lifeCount);
        }

        private void CompleteLevel(){
            LevelCompleted?.Invoke();
        }

        private IEnumerator WaitAndRespawn(){
            yield return new WaitForSeconds(1f);
            ReSpawn();
        }

        private void ReSpawn(){
            transform.position = _spawnPoint.position;
            _animator.SetState(PlayerAnimationState.Idle);
            _isMovementEnabled = true;
        }
    }
}