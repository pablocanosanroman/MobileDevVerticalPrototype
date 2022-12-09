using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody m_PlayerRb;
    public ConstantForce m_ConstantForce;
    public float m_Force;
    public bool m_IsMoving = false;

    private void Update()
    {
        //if(m_IsMoving)
        //{
        //    StartCoroutine(WaitForMove());
        //}
    }

    private void OnEnable()
    {
        m_IsMoving = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide");
        //if (Mathf.Abs(m_PlayerRb.velocity.magnitude) < 0.1f)
        //{
        //    Debug.Log("StopMoving");
        //    m_IsMoving = false;
        //    m_ConstantForce.force = Vector3.zero;
        //    m_PlayerRb.isKinematic = true;
        //}
        StartCoroutine(WaitForMove());
    }
    public void Movement(Vector3 direction)
    {
        Debug.Log("Move");
        m_IsMoving = true;
        m_PlayerRb.isKinematic = false;
        m_ConstantForce.force = direction * m_Force;

    }


    IEnumerator WaitForMove()
    {
        yield return new WaitForSeconds(0.1f);
        if (Mathf.Abs(m_PlayerRb.velocity.magnitude) < 0.1f)
        {
            Debug.Log("StopMoving");
            m_IsMoving = false;
            m_ConstantForce.force = Vector3.zero;
            m_PlayerRb.isKinematic = true;
        }
    }

}
