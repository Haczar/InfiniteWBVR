using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class LineInteraction : MonoBehaviour
{
    //Eraser Hover Interactions
    private void OnEraserHoverBegin(EraseSide eraseSide)
    {
        Debug.Log("Attempting to Erase Line...");

        Destroy(gameObject);
    }

    private void OnEraserHoverEnd(EraseSide eraseSide)
    {
        Debug.Log("Attempting to Erase Line...");

        Destroy(gameObject);
    }


    //Regs
    void Awake() { }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
