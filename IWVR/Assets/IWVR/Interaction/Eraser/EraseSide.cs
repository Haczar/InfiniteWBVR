using UnityEngine;
using Valve.VR.InteractionSystem;

public class EraseSide : MonoBehaviour
{
    private void Awake()
    {
    }


    void Start()
    {
        playerInstance = Player.instance;
    }

    private void UpdateHovering()
    {
       
    }


    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }

    private Player playerInstance;
}
