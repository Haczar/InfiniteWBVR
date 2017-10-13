using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public class LineDraw_Board : MonoBehaviour
{   //Alternative way of drawing lines on the board without using board interaction. (Only works if marker hover events are on the interactiable hover)
    public void activate  () { active     = true ; }
    public void deactivate() { lineActive = false; }

    void Start()
    {
        active     = false;
        lineActive = false;
    }

    void Update()
    {
        if (active)
        {
            lineNum++; line = new GameObject(lineName + lineNum.ToString());//Increments the lineNum value and then creates a new line object with the lineName and lineNum to gen name.

            line.transform.SetParent(gameObject.transform.Find("LineDump").transform);

            currentLine = line.gameObject.AddComponent<LineRenderer>();

            currentLine.startWidth = width;
            currentLine.endWidth   = width;

            currentLine.material = (Material)Resources.Load("Materials/Marker/markerDefault.mat");

            currentLine.startColor = color;
            currentLine.endColor   = color;

            index = 0;

            lineActive = true ;
            active     = false;
        }
        else if (lineActive)
        {
            currentLine.positionCount = index + 1;

            //currentLine.SetPosition(numCLicks, new Vector3(hand.transform.position.x, hand.transform.position.y, gameObject.transform.position.z - 0.01f));   //For using the hands instead.
            currentLine.SetPosition(index, new Vector3(marker.transform.position.x, marker.transform.position.y, gameObject.transform.position.z - 0.01f));

            index++;

            Debug.Log(currentLine.material);
        }
    }

    //Public
    public float width = 0.014f;   //Width of the line

    public Color color = Color.black;   //Color of the line.

    public GameObject board ;   //Board object to be used.
    public GameObject marker;

    public Hand hand;   //Hand that will create lines.

    //Private
    private bool active;
    private bool lineActive;

    private int index   = 0;   //Used for updating the line array index.
    private int lineNum = 0;   //Line Naming scheme numeral.

    private string lineName = "Line ";  //Part of Line Naming scheme.

    private GameObject line;   //Vector Line the line to be created.

    private LineRenderer currentLine;   //The current Line to be used within one update interval.

    private SteamVR_Controller.Device device;   //Used to get the input from the steamVR controller that was attached to the hand object.
}