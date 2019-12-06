using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start(){
        StartCoroutine(waitForSplashAnimation());
    }

    public void Play(){
        Debug.Log("hi");
        this.GetComponent<AudioSource>().Play();
        Debug.Log("clicked");
        SceneManager.LoadScene("Main");
    }

    public void Quit(){
        this.GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    private IEnumerator waitForSplashAnimation(){
        yield return new WaitForSeconds(1.0f);

        for(int i = 0; i < 4; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
