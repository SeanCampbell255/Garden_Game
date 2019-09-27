using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI;

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
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
    }
   
   void Pause ()
   {
	   pauseMenuUI.SetActive(true);
	   Time.timeScale = 0f;
	   GameIsPaused = true;
       GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = true;
        Debug.Log("Loading Menu.../Placeholder");
        // SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game.../Placeholder");
        Application.Quit();
    }
}
