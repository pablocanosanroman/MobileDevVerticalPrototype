using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Player;
    private Vector2 m_FingerStartPos;
    private Vector2 m_SwipeDelta;
    public bool m_FingerDown;
    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        if(enabled)
        {

            StartCoroutine(WaitForInput());

            if (m_FingerDown)
            {
                m_SwipeDelta = Input.touches[0].position - m_FingerStartPos;
            }

            if (m_SwipeDelta.magnitude > 125)
            {
                float x = m_SwipeDelta.x;
                float y = m_SwipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        Debug.Log("Left");
                        if (m_Player.m_ConstantForce.force == Vector3.zero)
                        {
                            
                            m_Player.Movement(Vector3.left);

                        }
                    }
                    else
                    {
                        if (m_Player.m_ConstantForce.force == Vector3.zero )
                        {
                            
                            m_Player.Movement(Vector3.right);
                        }
                        Debug.Log("Right");
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        if (m_Player.m_ConstantForce.force == Vector3.zero)
                        {
                            m_Player.Movement(Vector3.back);
                        }
                        Debug.Log("Down");
                    }
                    else
                    {
                        if (m_Player.m_ConstantForce.force == Vector3.zero )
                        {
                            m_Player.Movement(Vector3.forward);
                        }
                        Debug.Log("Up");
                    }
                }

                Reset();
            }
        }
        
    }

    IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(0.4f);
        
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                m_FingerStartPos = Input.touches[0].position;
                m_FingerDown = true;
                //m_FingerStartPosOffset = new Vector2(m_FingerStartPos.x + m_Offset, m_FingerStartPos.y + m_Offset);
                Debug.Log(m_FingerStartPos);
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }

        }

    }

    private void Reset()
    {
        m_FingerStartPos = Vector2.zero;
        m_SwipeDelta = Vector2.zero;
        m_FingerDown = false;
    }
}
