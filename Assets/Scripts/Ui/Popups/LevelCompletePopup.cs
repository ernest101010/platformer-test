using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups{
    public class LevelCompletePopupResult{
        public bool NextLevel{ get; }
        public bool MainMenu{ get; }
        public GameObject PopupGameObject{ get; }

        public LevelCompletePopupResult(GameObject popupGameObject, bool nextLevel, bool mainMenu){
            PopupGameObject = popupGameObject;
            NextLevel = nextLevel;
            MainMenu = mainMenu;
        }
    }

    [RequireComponent(typeof(PopupAnimatorBase))]
    public class LevelCompletePopup : MonoBehaviour, IPopup{
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _mainMenuButton;


        private PopupAnimatorBase _animator;

        public event Action<LevelCompletePopupResult> Closed;

        private void Awake(){
            _animator = GetComponent<PopupAnimatorBase>();
        }

        public void SetScore(int score){
            _scoreText.text = $"YOUR SCORE IS {score}";
        }

        private void OnEnable(){
            _nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnNextLevelButtonClicked(){
            Hide(() => { Closed?.Invoke(new LevelCompletePopupResult(gameObject, true, false)); });
        }
        
        private void OnMainMenuButtonClicked(){
            Hide(() => { Closed?.Invoke(new LevelCompletePopupResult(gameObject, false, true)); });
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
            _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}