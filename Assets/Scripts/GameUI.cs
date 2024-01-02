using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    [SerializeField] Transform Player;
    [SerializeField] Transform EndLine;
    [SerializeField] Slider slider;

    [SerializeField] private Text _timeText;

    private float _currentTime;
    [SerializeField] private float _duration;
    public GameObject TabtoRetryBtn;
    float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        maxDistance = getDistance();

        _currentTime = _duration;
        _timeText.text = _currentTime.ToString();
        StartCoroutine(CountdownTime());
        
    }

    private IEnumerator CountdownTime()
    {
        while (_currentTime >= 0)
        {
            _timeText.text = _currentTime.ToString();
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Player.position.x >= maxDistance && Player.position.x >= EndLine.position.x)
        {
            float distance = 1 - (getDistance() / maxDistance);
            setProgress(distance);
            //Debug.Log(getDistance());
            //Debug.Log(EndLine.position.x);
            
        }

        //Eğer zamanında bitiremezse main menü yüklenecek.
		if (_currentTime <= 0)
        {
            _playerMovement.m_Speed = 0f;
            _playerMovement.anim.SetBool("sad", true);
            Invoke("LoseSad", 5.0f);
            _playerMovement.anim.SetBool("fast", false);
        }

    }

    void LoseSad()
    {
        SceneManager.LoadScene("main");
    }
   

    float getDistance()
    {
         return Vector3.Distance(Player.position, EndLine.position);
	}

    void setProgress(float p)
	{
        slider.value = p;
	}
}
