using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views{
    public class GameplayScreen : ScreenBase<CanvasViewAnimator>{
        [SerializeField] private Button _pauseButton;
        [SerializeField] private List<Image> _remainingLifeImages = new();
        [SerializeField] private TextMeshProUGUI _scoreAmountText;

        public event Action PauseButtonClicked;

        protected override void OnBeforeShow(){
            base.OnBeforeShow();
            _pauseButton.onClick.AddListener(() => {
                PauseButtonClicked?.Invoke();
            });
        }

        public void UpdateLife(int remainingLifeCount){
            if (remainingLifeCount == 3){
                foreach (var image in _remainingLifeImages){
                    image.DOFade(1, 0.3f);
                }
            }
            else if(remainingLifeCount >= 0){
                _remainingLifeImages[remainingLifeCount].DOFade(0, 0.3f);
            }
        }

        protected override void OnBeforeHide(){
            base.OnBeforeHide();
            _pauseButton.onClick.RemoveAllListeners();
            
        }


        public void UpdateScore(int score){
            _scoreAmountText.text = score.ToString();
        }
    }
}