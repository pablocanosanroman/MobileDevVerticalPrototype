using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour
{
    public List<GameObject> m_RotateableObjects;
    private ObjectRotation m_ObjectToRotate;
    [SerializeField] private PlayerMovement m_Player;
    private void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            
            foreach(GameObject rotateableObject in m_RotateableObjects)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == rotateableObject && hit.collider.gameObject.CompareTag("RotateableObject"))
                    {
                        Debug.Log("hit");
                        m_ObjectToRotate = hit.collider.gameObject.GetComponentInParent<ObjectRotation>();
                        if(!m_ObjectToRotate.m_Rotating && !m_Player.m_IsMoving)
                        {
                            StartCoroutine(m_ObjectToRotate.RotationInterpolation());
                        }
                        
                    }
                }
                
            }
            //if(!m_ObjectToRotate.m_Rotating)
            //{
            //    if(Physics.Raycast(ray, out hit))
            //    {
            //        if(hit.collider.gameObject.CompareTag("RotateableObject"))
            //        {
            //            StartCoroutine(m_ObjectToRotate.RotationInterpolation());
            //        }
            //    }
            //}
        }
    }
}
