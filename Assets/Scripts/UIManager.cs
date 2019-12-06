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
    public Text highScoreText;
    public Text newHighScoreText;

    public Image comboNum;

    public GameObject pauseMenuUI;
    public GameObject[] disableOnNewHighScore;
    public GameObject highScoreBanner;

    public Sprite[] comboSprites;

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

    public void SetNewHighScore(int highScore)
    {
        highScoreText.text = "High Score: " + highScore;
        highScoreBanner.SetActive(true);

        newHighScoreText.gameObject.SetActive(true);
        newHighScoreText.text =  "" + highScore;

        foreach(GameObject obj in disableOnNewHighScore)
        {
            obj.SetActive(false);
        }
    }

    public void SetCombo(int combo)
    {
        if(combo == 0)
        {
            comboNum.gameObject.SetActive(false);
        } else
        {
            comboNum.gameObject.SetActive(true);
        }
        switch (combo)
        {
            case 0:
                comboNum.sprite = null;
                break;
            case 1:
                comboNum.sprite = comboSprites[0];
                sound.PlayCombo(1);
                break;
            case 2:
                comboNum.sprite = comboSprites[1];
                sound.PlayCombo(2);
                break;
            case 3:
                comboNum.sprite = comboSprites[2];
                sound.PlayCombo(3);
                break;
            case 4:
                comboNum.sprite = comboSprites[3];
                sound.PlayCombo(4);
                break;
            case 5:
                comboNum.sprite = comboSprites[4];
                sound.PlayCombo(4);
                break;
            case 6:
                comboNum.sprite = comboSprites[5];
                sound.PlayCombo(4);
                break;
        }
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
