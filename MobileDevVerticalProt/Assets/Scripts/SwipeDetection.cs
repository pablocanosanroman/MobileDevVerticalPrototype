using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Player;
    private Vector2 m_FingerStartPos;
    private Vector2 m_SwipeDelta;
    private bool m_FingerDown;

    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
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
        

        if(m_FingerDown)
        {
            m_SwipeDelta = Input.touches[0].position - m_FingerStartPos;
        }

        if(m_SwipeDelta.magnitude > 125)
        {
            float x = m_SwipeDelta.x;
            float y = m_SwipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if(x < 0)
                {
                    Debug.Log("Left");
                }
                else
                {
                    Debug.Log("Right");
                }
            }
            else
            {
                if (y < 0)
                {
                    Debug.Log("Down");
                }
                else
                {
                    Debug.Log("Up");
                }
            }

            Reset();
        }
    }

    private void Reset()
    {
        m_FingerStartPos = Vector2.zero;
        m_SwipeDelta = Vector2.zero;
        m_FingerDown = false;
    }
}
