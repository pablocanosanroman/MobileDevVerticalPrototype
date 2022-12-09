using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private float m_RotationSpeed;
    [SerializeField] private PlayerMovement m_Player;
    [SerializeField] private SwipeDetection m_SwipeScript;
    public bool m_Rotating;


    //private void Update()
    //{
    //    if(m_Rotating)
    //    {
            
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_Player.gameObject.transform.parent = null;
            Debug.Log("PLayerParentUnSet");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            m_Player.gameObject.transform.parent = gameObject.transform;
            //other.gameObject.transform.SetParent(gameObject.transform.parent);
            Debug.Log("PLayerParentSet");
        }
    }

    


    public IEnumerator RotationInterpolation()
    {
        
        m_Rotating = true;
        Vector3 degreesToRotate = new Vector3(0, 90.0f, 0);
        Quaternion rotationStart = transform.rotation;
        Quaternion rotationEnd = rotationStart * Quaternion.Euler(degreesToRotate);
        float rotationRate = 1.0f / m_RotationSpeed;
        float timeToRotate = 0.0f;
        while (timeToRotate < 1.0f)
        {
            timeToRotate += Time.deltaTime * rotationRate;
            transform.rotation = Quaternion.Lerp(rotationStart, rotationEnd, timeToRotate);
            
            yield return new WaitForEndOfFrame();
        }
        m_Player.enabled = true;
        m_Rotating = false;
    }
   
}
