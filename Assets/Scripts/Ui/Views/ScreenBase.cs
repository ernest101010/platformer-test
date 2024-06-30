using System;
using UnityEngine;

namespace Ui.Views {
    public interface IScreen {
        event Action BeforeShow;
        event Action AfterShow;
        event Action BeforeHide;
        event Action AfterHide;

        void Show();
        void Hide();
    }

    public interface IScreenAnimator {
        void Show(Action callback = null);
        void Hide(Action callback = null);
    }

    public abstract class ScreenBase<TAnimator> : MonoBehaviour, IScreen
        where TAnimator : MonoBehaviour, IScreenAnimator {
        [SerializeField] protected TAnimator _animator;

        public event Action BeforeShow;
        public event Action AfterShow;
        public event Action BeforeHide;
        public event Action AfterHide;

        public void Show() {
            OnBeforeShow();
            _animator.Show(OnAfterShow);
        }

        public void Hide() {
            OnBeforeHide();
            _animator.Hide(OnAfterHide);
        }

        protected virtual void OnBeforeShow() {
            BeforeShow?.Invoke();
        }

        protected virtual void OnAfterShow() {
            AfterShow?.Invoke();
        }

        protected virtual void OnBeforeHide() {
            BeforeHide?.Invoke();
        }

        protected virtual void OnAfterHide() {
            AfterHide?.Invoke();
        }

        protected virtual void Awake() {
            _animator = GetComponent<TAnimator>();
        }

        
    }
}