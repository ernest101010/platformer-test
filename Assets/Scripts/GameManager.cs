using System;
using Gameplay;
using Ui.Popups;
using Ui.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private GameplayScreen _gameplayScreen;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private LevelController _levelController;

    [SerializeField] private PopupManager _popupManager;

    private int _score;
    private void Awake(){
        _menuScreen.PlayButtonClicked += MenuScreenOnPlayButtonClicked;
        _menuScreen.InfoButtonClicked += MenuScreenOnInfoButtonClicked;
        _menuScreen.ExitButtonClicked += MenuScreenOnExitButtonClicked;
        
        _gameplayScreen.PauseButtonClicked += GameplayScreenOnPauseButtonClicked;

        _playerController.UpdateLife += PlayerControllerOnUpdateLife;
        _playerController.GetPoint += PlayerControllerOnGetPoint;
        _playerController.LevelCompleted += PlayerControllerOnLevelCompleted;
    }

    private void PlayerControllerOnLevelCompleted(){
        _popupManager.ShowLevelCompletePopup(_score,result => {
            if (result.NextLevel){
                Restart();
            }

            if (result.MainMenu){
                GotoMainMenu();
            }
        });
    }

    private void Start(){
        _menuScreen.Show();
    }

    private void PlayerControllerOnGetPoint(int count){
        _score += count;
        _gameplayScreen.UpdateScore(_score);
    }

    private void PlayerControllerOnUpdateLife(int lifeCount){
        _gameplayScreen.UpdateLife(lifeCount);
        if (lifeCount == 0){
            GameOver();
        }
    }

    private void GotoMainMenu(){
        _gameplayScreen.Hide();
        _menuScreen.Show();
        StopGame();
    }

    private void GameplayScreenOnPauseButtonClicked(){
        PauseGame();
    }

    private void MenuScreenOnExitButtonClicked(){
        throw new NotImplementedException();
    }

    private void MenuScreenOnInfoButtonClicked(){
        _popupManager.ShowMessagePopup("HOW TO PLAY",
            "USE ARROW FOR MOVEMENT \n USE SPACE FOR JUMP \n COLLECT COINS \n <b><color=#0000FF>AVOID </color></b> HAZARDS");
    }

    private void MenuScreenOnPlayButtonClicked(){
        StartGame();
        _menuScreen.Hide();
        _gameplayScreen.Show();
    }

    private void StartGame(){
        _levelController.ResetLevel();
        _playerController.ResetPlayer();
        _score = 0;
        _gameplayScreen.UpdateScore(_score);
    }

    private void PauseGame(){
        _playerController.EnableMovement(false);
        _popupManager.ShowPausePopup(result => {
            if (result.Resume){
                ResumeGame();
            }

            if (result.MainMenu){
                GotoMainMenu();
            }
        });
    }

    private void ResumeGame(){
        _playerController.EnableMovement(true);
    }

    private void StopGame(){
        
    }

    private void GameOver(){
        _popupManager.ShowGameOverPopup(_score,result => {
            if (result.Restart){
                Restart();
            }

            if (result.MainMenu){
                GotoMainMenu();
            }
        });
    }

    private void Restart(){
        StartGame();
    }

    private void OnDestroy(){
        
    }
}