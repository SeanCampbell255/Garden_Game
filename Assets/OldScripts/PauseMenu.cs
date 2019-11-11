using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI;
    public GameObject player;

    private PlayerController playerController;

    private void Start(){
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else 
			{
				Pause();
			}
		}
 
   }
   
   public void Resume ()
   {
	   pauseMenuUI.SetActive(false);
	   Time.timeScale = 1f;
	   GameIsPaused = false;
       playerController.enabled = true;
    }
   
   void Pause ()
   {
	   pauseMenuUI.SetActive(true);
	   Time.timeScale = 0f;
	   GameIsPaused = true;
       playerController.enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        playerController.enabled = true;
        Debug.Log("Loading Menu.../Placeholder");
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game.../Placeholder");
        Application.Quit();
    }
}
