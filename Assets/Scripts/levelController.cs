using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelController : MonoBehaviour
{
    [SerializeField] private Text _levelText;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetString("Level", sceneName);
        Debug.Log(PlayerPrefs.GetString("Level"));
        
        //Debug.Log("Tiklandi");
    }

    
    public void LoadSceneMain()
	{
        if (PlayerPrefs.HasKey("Level"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Level"));
        }
        else
        {
            SceneManager.LoadScene("1");
        }
    }

    void Start()
	{
        _levelText.text = SceneManager.GetActiveScene().name;
    }

}
