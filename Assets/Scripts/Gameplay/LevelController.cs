using UnityEngine;

namespace Gameplay{
    public class LevelController : MonoBehaviour{
        [SerializeField] private GameObject _coinsPrefab;
        [SerializeField] private GameObject _patrolsPrefab;

        private GameObject _coins;
        private GameObject _patrols;

        public void InitLevel(){
            _coins = Instantiate(_coinsPrefab);
            _patrols = Instantiate(_patrolsPrefab);
        }

        public void ResetLevel(){
            Destroy(_coins);
            Destroy(_patrols);
            InitLevel();
        }
    }
}