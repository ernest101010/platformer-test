using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups{
    public class PopupManager : MonoBehaviour {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GraphicRaycaster _graphicRaycaster;
        [SerializeField] private Transform _container;
        [SerializeField] private MessagePopup _messagePopupPrefab;
        [SerializeField] private GameOverPopup _gameOverPopupPrefab;
        [SerializeField] private LevelCompletePopup _levelCompletePopupPrefab;
        [SerializeField] private PausePopup _pausePopupPrefab;
        [SerializeField] private Image _dimmer;
        [SerializeField] private float _dimmerDuration;
        [SerializeField] private Ease _dimmerEase;
        [SerializeField] private Button _dimmerButton;

        private IPopup _currentPopup;

        public void ShowMessagePopup(string title, string message) {
            EnableCanvas();
            ShowDimmer();
            
            var popup = Instantiate(_messagePopupPrefab).GetComponent<MessagePopup>();
            popup.transform.SetParent(_container);
            popup.transform.localPosition = Vector3.zero;
            popup.SetInfo(title, message);
            _currentPopup = popup;
            popup.Show();
            popup.Closed += closeResult => {
                HideDimmer(DisableCanvas);
                Destroy(closeResult.PopupGameObject);
            };
        }
        
        public void ShowPausePopup(Action<PausePopupResult> callback) {
            EnableCanvas();
            ShowDimmer();
            
            var popup = Instantiate(_pausePopupPrefab).GetComponent<PausePopup>();
            popup.transform.SetParent(_container);
            popup.transform.localPosition = Vector3.zero;
            _currentPopup = popup;
            popup.Show();
            popup.Closed += closeResult => {
                HideDimmer(DisableCanvas);
                callback?.Invoke(closeResult);
                Destroy(closeResult.PopupGameObject);
            };
        }
        
        public void ShowGameOverPopup(int finalScore, Action<GameOverPopupResult> callback) {
            EnableCanvas();
            ShowDimmer();
            
            var popup = Instantiate(_gameOverPopupPrefab).GetComponent<GameOverPopup>();
            popup.transform.SetParent(_container);
            popup.transform.localPosition = Vector3.zero;
            popup.SetScore(finalScore);
            _currentPopup = popup;
            popup.Show();
            popup.Closed += closeResult => {
                HideDimmer(DisableCanvas);
                callback?.Invoke(closeResult);
                Destroy(closeResult.PopupGameObject);
            };
        }
        
        public void ShowLevelCompletePopup(int finalScore, Action<LevelCompletePopupResult> callback) {
            EnableCanvas();
            ShowDimmer();
            
            var popup = Instantiate(_levelCompletePopupPrefab).GetComponent<LevelCompletePopup>();
            popup.transform.SetParent(_container);
            popup.transform.localPosition = Vector3.zero;
            popup.SetScore(finalScore);
            _currentPopup = popup;
            popup.Show();
            popup.Closed += closeResult => {
                HideDimmer(DisableCanvas);
                callback?.Invoke(closeResult);
                Destroy(closeResult.PopupGameObject);
            };
        }

        private void EnableCanvas() {
            _canvas.enabled = true;
            _graphicRaycaster.enabled = true;
        }

        private void DisableCanvas() {
            _canvas.enabled = false;
            _graphicRaycaster.enabled = false;
        }

        private void ShowDimmer() {
            _dimmer.DOKill();

            _dimmer.DOFade(.5f, _dimmerDuration)
                .SetEase(_dimmerEase)
                .SetTarget(_dimmer)
                .OnComplete(() => _dimmer.raycastTarget = true);
        }


        private void HideDimmer(Action callback) {
            _dimmer.DOKill();
            _dimmer.raycastTarget = false;
            _dimmer.DOFade(0, _dimmerDuration)
                .SetEase(_dimmerEase)
                .SetTarget(_dimmer)
                .OnComplete(() => { callback?.Invoke(); });
        }

        private void Start() {
            _dimmerButton.onClick.AddListener(() => { _currentPopup?.Dismiss(); });
        }

        private void OnDestroy() {
            _dimmerButton.onClick.RemoveAllListeners();
        }
    }
}