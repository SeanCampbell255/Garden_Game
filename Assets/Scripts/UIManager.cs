using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text gameOverText;
    public Text scoreText;
    public Text basketText;
    public Text comboText;
    public Text highScoreText;

    public GameObject pauseMenuUI;

    public PlayerController playerController;
    public SoundController sound;

    private bool gameIsPaused = false;
    private bool canUnpause = true;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (gameIsPaused){
                Resume();
                sound.PlaySelect();
            }
            else{
                Pause();
            }
        }
    }

    public void SetHighScore(int highScore)
    {
        highScoreText.text = "High Score: " + highScore;
    }

    public void SetCombo(int combo)
    {
        comboText.text = "Combo: " + combo;
    }

    public void SetBasketText(int pieces){
        basketText.text = "" + pieces + " X";
    }

    public void SetScore(int score){
        scoreText.text = "" + score;
    }

    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        Pause();
        canUnpause = false;
    }

    public void Resume(){
        if (canUnpause){
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
            playerController.enabled = true;
        }
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        playerController.enabled = false;
        sound.PlaySelect();
    }

    public void LoadMenu(){
        Time.timeScale = 1f;
        playerController.enabled = true;
        Debug.Log("Loading Menu.../Placeholder");
        SceneManager.LoadScene("Menu");
        sound.PlayBonk();
    }

    public void QuitGame(){
        Debug.Log("Quitting Game.../Placeholder");
        sound.PlayBonk();
        Application.Quit();
    }

    public void Retry(){
        Time.timeScale = 1f;
        sound.PlayBonk();
        SceneManager.LoadScene("Main");
    }
}
