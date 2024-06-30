using System;
using DG.Tweening;
using UnityEngine;

namespace Ui.Popups{
    public interface IPopupAnimatorBase{
        void Show(Action callback = null);
        void Hide(Action callback = null);
    }

    public class PopupAnimatorBase : MonoBehaviour, IPopupAnimatorBase{
        [SerializeField] private RectTransform _popupTransform;
        [SerializeField] private float _hiddenPosY = -15;
        [SerializeField] private Ease _showEase = Ease.InOutSine;
        [SerializeField] private Ease _hideEase = Ease.OutSine;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _duration = .2f;

        public virtual void Show(Action callback = null){
            this.DOKill();

            var posTween = _popupTransform.DOAnchorPosY(0, _duration)
                .SetEase(_showEase)
                .From(new Vector2(_popupTransform.anchoredPosition.x, _hiddenPosY));
            var fadeTween = _canvasGroup.DOFade(1, _duration)
                .SetEase(_showEase)
                .From(0);

            var sequence = DOTween.Sequence();
            sequence.Join(posTween);
            sequence.Join(fadeTween);
            sequence.OnComplete(() => callback?.Invoke());
            sequence.SetTarget(this);
        }

        public virtual void Hide(Action callback = null){
            this.DOKill();

            var posTween = _popupTransform.DOAnchorPosY(_hiddenPosY, _duration)
                .SetEase(_hideEase);
            var fadeTween = _canvasGroup.DOFade(0, _duration)
                .SetEase(_hideEase);

            var sequence = DOTween.Sequence();
            sequence.Join(posTween);
            sequence.Join(fadeTween);
            sequence.OnComplete(() => { callback?.Invoke(); });
            sequence.SetTarget(this);
        }
    }
}