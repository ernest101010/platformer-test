using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups{
    public class GameOverPopupResult{
        public bool Restart{ get; }
        public bool MainMenu{ get; }
        public GameObject PopupGameObject{ get; }

        public GameOverPopupResult(GameObject popupGameObject, bool restart, bool mainMenu){
            PopupGameObject = popupGameObject;
            Restart = restart;
            MainMenu = mainMenu;
        }
    }

    [RequireComponent(typeof(PopupAnimatorBase))]
    public class GameOverPopup : MonoBehaviour, IPopup{
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;


        private PopupAnimatorBase _animator;

        public event Action<GameOverPopupResult> Closed;

        private void Awake(){
            _animator = GetComponent<PopupAnimatorBase>();
        }

        public void SetScore(int score){
            _scoreText.text = $"YOUR SCORE IS {score}";
        }

        private void OnEnable(){
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnRestartButtonClicked(){
            Hide(() => { Closed?.Invoke(new GameOverPopupResult(gameObject, true, false)); });
        }
        
        private void OnMainMenuButtonClicked(){
            Hide(() => { Closed?.Invoke(new GameOverPopupResult(gameObject, false, true)); });
        }

        public void Show(Action callback = null){
            _animator.Show(() => { callback?.Invoke(); });
        }

        public void Hide(Action callback) {
            _animator.Hide(() => {
                callback?.Invoke();
                gameObject.SetActive(false);
            });
        }
        
        public void Dismiss(Action callback){
            OnMainMenuButtonClicked();
        }
        
        private void OnDisable(){
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }
    }
}