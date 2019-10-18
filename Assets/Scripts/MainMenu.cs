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
        Debug.Log("clicked");
        SceneManager.LoadScene("Main");
    }

    public void Quit(){
        Application.Quit();
    }

    private IEnumerator waitForSplashAnimation(){
        yield return new WaitForSeconds(1.0f);

        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
