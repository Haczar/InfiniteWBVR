using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineDraw : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger))
        {
            line = new GameObject();

            currentLine = line.AddComponent<LineRenderer>();

            currentLine.startWidth = width;
            currentLine.endWidth   = width;

            currentLine.startColor = Color.grey;
            currentLine.endColor   = Color.grey;

            //line = (GameObject)Instantiate(Resources.Load("Assets\\IWVR\\Interaction\\LineRender\\LineRenderObj.prefab"));

            numCLicks = 0;
        }
        else if (  device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)
                && trackedObj.transform.position.x <= board.transform.position.x 
                && trackedObj.transform.position.y <= board.transform.position.y)
        {
            currentLine.positionCount = numCLicks + 1;

            currentLine.SetPosition(numCLicks, new Vector3(trackedObj.transform.position.x, trackedObj.transform.position.y, board.transform.position.z-0.01f));

            numCLicks++;
        }
	}

    //Public
    public float width = 0.05f;

    public Color color = Color.black;

    public SteamVR_TrackedObject trackedObj;

    public GameObject board;

    //Private
    private int   numCLicks = 0;

    private GameObject line;

    private LineRenderer currentLine;

    private SteamVR_Controller.Device device;
}