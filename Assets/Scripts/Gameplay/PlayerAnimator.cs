using System;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using UnityEngine;


namespace Gameplay{

    public enum PlayerAnimationState{
        Idle = 0,
        Walk = 1,
        Run = 2,
        Jump = 3,
        Hit = 4,
        Dead = 5,
    }

    public class PlayerAnimator : MonoBehaviour{
        [SerializeField] private SkeletonAnimation _skeletonAnimation;
        private Spine.AnimationState _animationState;

        private Dictionary<PlayerAnimationState, string> _stateMap = new(){
            { PlayerAnimationState.Idle, "idle_3" },
            { PlayerAnimationState.Walk, "walk_shield" },
            { PlayerAnimationState.Run, "run_shield" },
            { PlayerAnimationState.Jump, "jump" },
            { PlayerAnimationState.Hit, "hit" },
            { PlayerAnimationState.Dead, "dead" },

        };

        private PlayerAnimationState _currentState;
        private Action AnimationEnd;

        private void Awake(){
            _animationState = _skeletonAnimation.AnimationState;
            _animationState.End += delegate(TrackEntry entry){ AnimationEnd?.Invoke(); };
        }

        public void SetState(PlayerAnimationState state, Action callback = null){
            AnimationEnd = callback;
            if (_currentState == state){
                return;
            }

            _currentState = state;

            bool isLoop = state == PlayerAnimationState.Idle || state == PlayerAnimationState.Run;
            _animationState.SetAnimation(0, _stateMap[state], isLoop);
        }
    }
}