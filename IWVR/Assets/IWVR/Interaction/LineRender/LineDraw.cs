using System;
using UnityEngine               ;
using Valve.VR.InteractionSystem;


public class LineDraw : MonoBehaviour   //The classic way of doing line draw.
{
    private void Start()
    {
       // lineRenObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/IWVR/Interaction/LineRenderObj.prefab");
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)hand.controller.index);

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))   //Checks to see if trigger has been pressed down.
        {
            lineNum++; line = new GameObject(lineName + lineNum.ToString());//Increments the lineNum value and then creates a new line object with the lineName and lineNum to gen name.

            line.transform.SetParent(gameObject.transform.Find("LineDump").transform);

            currentLine = line.gameObject.AddComponent<LineRenderer>();

            currentLine.startWidth = width;
            currentLine.endWidth = width;

            currentLine.material = (Material)Resources.Load("Materials/Marker/markerDefault.mat");

            currentLine.startColor = color;
            currentLine.endColor = color;

            index = 0;
        }
        else if (  device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)   //Checks to see if hand is within bounds, and trigger is still held down.
                && hand.transform.position.x <=  board.transform.localScale.x / 2 + board.transform.position.x
                && hand.transform.position.x >= -board.transform.localScale.x / 2 + board.transform.position.x
                && hand.transform.position.y <=  board.transform.localScale.y / 2 + board.transform.position.y
                && hand.transform.position.y >= -board.transform.localScale.y / 2 + board.transform.position.y)
        {
            currentLine.positionCount = index + 1;

            currentLine.SetPosition(index, new Vector3(hand.transform.position.x, hand.transform.position.y, board.transform.position.z - 0.01f));

            index++;
        }
    }

    //Public
    public float width = 0.014f;   //Width of the line

    public Color color = Color.black;   //Color of the line.

    public GameObject board;   //Board object to be used.

    public Hand hand;   //Hand that will create lines.

    //Private
    private int index   = 0;   //Used for updating the line array index.
    private int lineNum = 0;   //Line Naming scheme numeral.

    private string lineName = "Line ";  //Part of Line Naming scheme.

    private GameObject line;   //Vector Line the line to be created.

    private LineRenderer currentLine;   //The current Line to be used within one update interval.

    private GameObject lineRenObj;

    private SteamVR_Controller.Device device;   //Used to get the input from the steamVR controller that was attached to the hand object.
}