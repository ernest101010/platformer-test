using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views{
    public class MenuScreen : ScreenBase<CanvasViewAnimator>{
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _exitButton;

        public event Action PlayButtonClicked;
        public event Action InfoButtonClicked;
        public event Action ExitButtonClicked;

        protected override void Awake(){
            base.Awake();
        }

        protected override void OnBeforeShow(){
            base.OnBeforeShow();
            _playButton.onClick.AddListener(() => PlayButtonClicked?.Invoke());
            _infoButton.onClick.AddListener(() => InfoButtonClicked?.Invoke());
            _exitButton.onClick.AddListener(() => ExitButtonClicked?.Invoke());
        }

        protected override void OnAfterShow(){
            base.OnAfterShow();
            AnimateButtons();
        }

        private void AnimateButtons(){
            _playButton.transform.localScale = Vector3.zero;
            _infoButton.transform.localScale = Vector3.zero;
            _exitButton.transform.localScale = Vector3.zero;
            var playButtonTween = _playButton.transform
                .DOScale(1, 0.2f)
                .SetEase(Ease.InOutCubic);
            var infoButtonTween = _infoButton.transform
                .DOScale(1, 0.2f)
                .SetEase(Ease.InOutCubic);
            var exitButtonTween = _exitButton.transform
                .DOScale(1, 0.2f)
                .SetEase(Ease.InOutCubic);

            var sequence = DOTween.Sequence();
            sequence.Join(playButtonTween);
            sequence.AppendInterval(0.05f);
            sequence.Join(infoButtonTween);
            sequence.AppendInterval(0.05f);
            sequence.Join(exitButtonTween);
            sequence.OnComplete(() => {  });
        }

        protected override void OnBeforeHide(){
            base.OnBeforeHide();
            _playButton.onClick.RemoveAllListeners();
            _infoButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        protected override void OnAfterHide(){
            base.OnAfterHide();
            _playButton.transform.localScale = Vector3.zero;
            _infoButton.transform.localScale = Vector3.zero;
            _exitButton.transform.localScale = Vector3.zero;
        }
    }
}