using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    public GameObject mainMenuUI;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayMenu()
    {
        //Load the play menu (will feature resume game level select, and endless mode)
        Debug.Log("Loading play menu.../Placeholder");

    }

    public void LoadOptionsMenu()
    {
        //load options menu (will feature audio settings, resolution, and possibly more)
        Debug.Log("Loading options menu.../Placeholder");

    }

    public void LoadCredits()
    {
        //load credits (will feature our names scrolling down slowly with some music in the background.)
        Debug.Log("Loading credits.../Placeholder");

    }

    public void QuitToDesktop()
    {
        //quits to desktop for pc only. Can be removed or quit to title screen for consoles/mobile.
        Debug.Log("Quiting to desktop.../Placeholder");

    }
}
