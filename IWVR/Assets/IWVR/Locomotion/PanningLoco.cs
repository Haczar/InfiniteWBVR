using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PanningLoco : MonoBehaviour
{

	// Use this for initialization
	void Start () 
    {
        active = false;

        if   (locoController == controllers.left) { locomotionController = GetComponent<SteamVR_TrackedController>(); }
        else                                      { locomotionController = GetComponent<SteamVR_TrackedController>(); }

        if   (boardController == controllers.right) { boardInteractController = GetComponent<SteamVR_TrackedController>(); }
        else                                        { boardInteractController = GetComponent<SteamVR_TrackedController>(); }
	}

    // Update is called once per frame
    void Update()
    {

    }

    public enum controllers { left, right };
   
    protected bool active;

    public controllers locoController ;
    public controllers boardController;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_TrackedController locomotionController   ;
    private SteamVR_TrackedController boardInteractController;
}