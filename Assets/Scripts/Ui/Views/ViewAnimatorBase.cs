using System;
using DG.Tweening;
using UnityEngine;

namespace Ui.Views {
    public static class ScreensShowParameters {
        public const float SHOW_DURATION = .15f;
        public const Ease SHOW_EASE = Ease.Linear;
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class ViewAnimatorBase : MonoBehaviour, IScreenAnimator {
        [SerializeField]private CanvasGroup _canvasGroup;

        protected virtual void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show(Action callback = null) {
            this.DOKill();

            _canvasGroup.DOFade(1, ScreensShowParameters.SHOW_DURATION)
                .SetEase(ScreensShowParameters.SHOW_EASE)
                .From(0)
                .OnComplete(() => { callback?.Invoke(); })
                .SetTarget(this);
        }

        public virtual void Hide(Action callback = null) {
            this.DOKill();

            _canvasGroup.alpha = 0;
            callback?.Invoke();
        }
    }
}