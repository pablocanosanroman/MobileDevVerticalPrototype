using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_Player;
    public List<GameObject> m_RotateableObjects;
    private ObjectRotation m_ObjectToRotate;
    private Vector2 m_FingerStartPos;
    private Vector2 m_SwipeDelta;
    public bool m_FingerDown;
    private RaycastHit m_Hit;
    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        StartCoroutine(WaitForInput());

        //if (m_Player.m_IsMoving)
        //{
        //    m_ObjectToRotate = null;
        //}
            

        

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            foreach (GameObject rotateableObject in m_RotateableObjects)
            {
                if (Physics.Raycast(ray, out m_Hit))
                {
                    if (m_Hit.collider.gameObject == rotateableObject && m_Hit.collider.gameObject.CompareTag("RotateableObject") && !m_Player.m_IsMoving)
                    {
                        m_ObjectToRotate = m_Hit.collider.gameObject.GetComponentInParent<ObjectRotation>();
                        if (!m_ObjectToRotate.m_Rotating)
                        {
                            m_Player.enabled = false;
                            StartCoroutine(m_ObjectToRotate.RotationInterpolation());
                            m_ObjectToRotate = null;
                        }

                    }
                }

            }
        }

        if (m_FingerDown)
        {
            m_SwipeDelta = Input.touches[0].position - m_FingerStartPos;
        }

        if (m_SwipeDelta.magnitude > 125 && m_Player.enabled)
        {
               
            float x = m_SwipeDelta.x;
            float y = m_SwipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0)
                {
                    if (m_Player.m_ConstantForce.force == Vector3.zero)
                    {
                        Debug.Log("IsMoving");
                            
                        m_Player.Movement(Vector3.left);

                    }
                }
                else
                {
                    if (m_Player.m_ConstantForce.force == Vector3.zero )
                    {
                            
                        m_Player.Movement(Vector3.right);
                    }
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
                }
                else
                {
                    if (m_Player.m_ConstantForce.force == Vector3.zero )
                    {
                        m_Player.Movement(Vector3.forward);
                    }
                }
            }

                
            Reset();
        }
        
        
    }

    IEnumerator WaitForInput()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                if(m_Player.enabled)
                {
                    m_FingerStartPos = Input.touches[0].position;
                    m_FingerDown = true;
                }
                

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
