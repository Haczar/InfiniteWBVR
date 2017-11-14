using UnityEngine               ;
using Valve.VR.InteractionSystem;
using IWVR;

public class MarkerTip : MonoBehaviour
{
    private void Awake()
    {  }


    void Start ()
    {
        playerInstance = Player.instance;
	}

    private void UpdateHovering()
    {

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        BoardInteraction bi = collision.gameObject.GetComponent<BoardInteraction>();
        if(bi != null)
        {
            bi.OnMarkerHoverBegin(this);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        BoardInteraction bi = collision.gameObject.GetComponent<BoardInteraction>();
        if (bi != null)
        {
            bi.OnMarkerHoverUpdate(this);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        BoardInteraction bi = collision.gameObject.GetComponent<BoardInteraction>();
        if (bi != null)
        {
            bi.OnMarkerHoverEnd(this);
        }
    }

    //Public

    //Private
    private Player playerInstance;
}
