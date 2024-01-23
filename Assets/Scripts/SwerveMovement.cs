using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    private PlayerMovement playerMovement;
    private bool isFinished = false;

    [SerializeField] private float swerveSpeed = 1.5f;
    [SerializeField] private float maxSwerveAmount = 10f;

    private float maxLeftSwerve = -3f; // Z ekseninde maksimum sola gidilecek mesafe
    private float maxRightSwerve = 3f; // Z ekseninde maksimum sa�a gidilecek mesafe

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!isFinished)
        {
            float swerveAmount = _swerveInputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);

            // Hedef pozisyonu belirle, ancak s�n�rlar i�inde tut
            float targetZPosition = Mathf.Clamp(transform.position.z + swerveAmount, maxLeftSwerve, maxRightSwerve);
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, targetZPosition);

            // Yumu�ak ge�i�le karakterin yeni pozisyonuna git
            transform.position = Vector3.Lerp(transform.position, targetPosition, swerveSpeed * Time.deltaTime);
        }
    }

    public void SetFinished(bool value)
    {
        isFinished = value;
    }
}
