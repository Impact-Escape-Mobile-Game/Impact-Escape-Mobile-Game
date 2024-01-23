using System.Collections;
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

        // Başlangıçta süreyi ayarla ve sayacı başlat
        _currentTime = _duration;
        _timeText.text = _currentTime.ToString();
        StartCoroutine(CountdownTime());
    }

    private IEnumerator CountdownTime()
    {
        while (_currentTime >= 0 && !hasFinished)
        {
            _timeText.text = _currentTime.ToString();
            yield return new WaitForSeconds(1f);
            _currentTime--;
        }

        if (!hasFinished)
        {
            _playerMovement.m_Speed = 0f;
            _playerMovement.anim.SetBool("sad", true);

            // Animasyonun tamamlanmasını bekleyin
            float animationLength = _playerMovement.anim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationLength);

            _playerMovement.anim.SetBool("fast", false);

            // Animasyonun tamamlanmasından sonra main sahnesine geç
            SceneManager.LoadScene("main");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Player.position.x >= maxDistance && Player.position.x >= EndLine.position.x)
        {
            float distance = 1 - (getDistance() / maxDistance);
            setProgress(distance);
        }

        // Her bölüm başında süreyi sıfırla ve animasyonu oynat
        if (Player.position.x < maxDistance || Player.position.x < EndLine.position.x)
        {
            if (hasFinished)
            {
                // Her bölüm başında süreyi sıfırla
                _currentTime = _duration;
                StartCoroutine(CountdownTime());
                hasFinished = false;
            }
        }
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
