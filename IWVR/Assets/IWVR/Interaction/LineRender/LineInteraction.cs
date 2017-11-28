using UnityEngine;
using Valve.VR.InteractionSystem;

public class LineInteraction : MonoBehaviour
{
    //Eraser Hover Interactions
    private void OnTriggerEnter(Collider other)
    {
        EraseSide es = other.gameObject.GetComponent<EraseSide>();
        if (es != null)
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    Debug.Log("Attempting to Erase Line...");

    //    Destroy(gameObject);
    //}

    private void Awake()
    {
        Rigidbody r = Util.FindOrAddComponent<Rigidbody>(this.gameObject);
        r.useGravity = false;
        r.isKinematic = true;
        
    }
}
