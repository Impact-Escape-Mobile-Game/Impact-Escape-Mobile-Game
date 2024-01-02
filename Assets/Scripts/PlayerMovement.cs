using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float m_Speed;
    public Animator anim;
    public GameObject TabtoNextBtn;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //Set the speed of the GameObject
        m_Speed = 9.0f;
    }
    
    void FixedUpdate()
    {
        rb.velocity = Vector3.left * m_Speed;
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Boost")
        {
            m_Speed = 12.0f;
            anim.SetBool("fast", true);

            StartCoroutine(SlowAfterAWhileCoroutine());
        }

        if (col.gameObject.tag == "Obstacle")
        {

            m_Speed = 5.0f;
            anim.SetBool("slow", true);

            StartCoroutine(FastAfterAWhileCoroutine());

        }
        
        if (col.gameObject.tag == "Finish")
        {
            anim.SetTrigger("dancing");
            m_Speed = 0f;
           // TabtoNextBtn.SetActive(true);

        }
    }

    private IEnumerator SlowAfterAWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        m_Speed = 9.0f;
        anim.SetBool("fast", false);
    }

    
    private IEnumerator FastAfterAWhileCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        m_Speed = 9.0f;
        anim.SetBool("slow", false);
    }
    
    
}
