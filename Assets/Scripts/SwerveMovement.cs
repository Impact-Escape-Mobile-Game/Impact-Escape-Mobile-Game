using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
        [SerializeField] private float swerveSpeed = 0.5f;
        [SerializeField] private float maxSwerveAmount = 1f;
        
        private void Awake()
        {
            _swerveInputSystem = GetComponent<SwerveInputSystem>();
        }
    
        private void Update()
        {
            float swerveAmount =  _swerveInputSystem.MoveFactorX;
            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.position =  Vector3.Lerp(transform.position, transform.position +  swerveAmount * Vector3.forward, Time.deltaTime * swerveSpeed);
        }
}
