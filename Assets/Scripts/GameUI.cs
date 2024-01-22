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

    float maxDistance;
    bool hasFinished = false; // Flag to track if the game has already finished

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
        while (_currentTime >= 0 && !hasFinished) // Check the hasFinished flag
        {
            _timeText.text = _currentTime.ToString();
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }
        yield return null;
    }

    // Update is called once per frame
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

    // Eğer zamanında bitiremezse main menü yüklenecek.
    if (_currentTime <= 0 && (Player.position.x < maxDistance || Player.position.x < EndLine.position.x))
    {
        if (!hasFinished) // Check if the game has not already finished
        {
            _playerMovement.m_Speed = 0f;
            _playerMovement.anim.SetBool("sad", true);
            Invoke("LoseSad", 0.4f);
            _playerMovement.anim.SetBool("fast", false);
            hasFinished = true; // Set the flag to true only when the time runs out
        }
    }
    else if (Player.position.x < maxDistance || Player.position.x < EndLine.position.x)
    {
        // Reset the hasFinished flag if the player is not at the finish line and time is not up
        hasFinished = false;
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

    // Add a public method to set the hasFinished flag
    public void SetGameFinished()
    {
        hasFinished = true;
    }
}
