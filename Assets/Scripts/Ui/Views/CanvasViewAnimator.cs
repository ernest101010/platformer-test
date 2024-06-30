using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views {
    [RequireComponent(typeof(Canvas))]
    public class CanvasViewAnimator : ViewAnimatorBase {
        private Canvas _canvas;
        private GraphicRaycaster _graphicRaycaster;

        private bool _hasGraphicRaycaster;

        public override void Show(Action callback = null) {
            SetVisible(true);
            base.Show(callback);
        }

        private void SetVisible(bool isVisible) {
            _canvas.enabled = isVisible;
            if (_hasGraphicRaycaster) {
                _graphicRaycaster.enabled = isVisible;
            }
        }

        public override void Hide(Action callback = null) {
            base.Hide(() => {
                SetVisible(false);
                callback?.Invoke();
            });
        }

        protected override void Awake() {
            base.Awake();
            _canvas = GetComponent<Canvas>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        private void Start() {
            _hasGraphicRaycaster = _graphicRaycaster != null;
        }
    }
}