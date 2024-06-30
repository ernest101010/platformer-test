using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Popups{
    public interface IPopup{
        void Show(Action callback = null);
        void Hide(Action callback = null);
        void Dismiss(Action callback = null);
    }

    public class PopupCloseResultBase{
        public bool Dismissed{ get; }
        public GameObject PopupGameObject{ get; }

        public PopupCloseResultBase(GameObject popupGameObject, bool dismissed){
            PopupGameObject = popupGameObject;
            Dismissed = dismissed;
        }
    }

    [RequireComponent(typeof(PopupAnimatorBase))]
    public class MessagePopup : MonoBehaviour, IPopup{
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _messageText;
        [SerializeField] private Button _closeButton;


        private PopupAnimatorBase _animator;

        public event Action<PopupCloseResultBase> Closed;

        private void Awake(){
            _animator = GetComponent<PopupAnimatorBase>();
        }

        public void SetInfo(string title, string message){
            _titleText.text = title;
            _messageText.text = message;
            // if (_messageText != null)
            //     _messageText.gameObject.SetActive(false);

        }

        private void OnEnable(){
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnCloseButtonClicked(){
            Hide();
        }

        public void Show(Action callback = null){
            _animator.Show(() => { callback?.Invoke(); });
        }

        public void Hide(Action callback = null){
            _animator.Hide(() => {
                Closed?.Invoke(new PopupCloseResultBase(gameObject, false));
                callback?.Invoke();
            });
        }

        public void Dismiss(Action callback = null){
            _animator.Hide(() => {
                Closed?.Invoke(new PopupCloseResultBase(gameObject, true));
                callback?.Invoke();
            });
        }

        private void OnDisable(){
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }
    }
}