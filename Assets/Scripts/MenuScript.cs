using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    public void InfoButton()
    {
        SceneManager.LoadScene(1);
    }


    public void MenuButton() { 
        SceneManager.LoadScene(0);
    }
}
