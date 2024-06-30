using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups{
    public class PausePopupResult{
        public bool Resume{ get; }
        public bool MainMenu{ get; }
        public GameObject PopupGameObject{ get; }

        public PausePopupResult(GameObject popupGameObject, bool resume, bool mainMenu){
            PopupGameObject = popupGameObject;
            Resume = resume;
            MainMenu = mainMenu;
        }
    }

    [RequireComponent(typeof(PopupAnimatorBase))]
    public class PausePopup : MonoBehaviour, IPopup{
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;


        private PopupAnimatorBase _animator;

        public event Action<PausePopupResult> Closed;

        private void Awake(){
            _animator = GetComponent<PopupAnimatorBase>();
        }

        private void OnEnable(){
            _resumeButton.onClick.AddListener(OnResumeButtonClicked);
            _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnResumeButtonClicked(){
            Hide(() => { Closed?.Invoke(new PausePopupResult(gameObject, true, false)); });
        }
        
        private void OnMainMenuButtonClicked(){
            Hide(() => { Closed?.Invoke(new PausePopupResult(gameObject, false, true)); });
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
            OnResumeButtonClicked();
        }
        
        private void OnDisable(){
            _resumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            _mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}