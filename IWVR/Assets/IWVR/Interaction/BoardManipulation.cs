using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManipulation : MonoBehaviour
{
    void moveBoard(Vector3 translation)
    {
<<<<<<< HEAD
        void moveBoard(Vector3 translation)
        { Board.gameObject.transform.position += translation; }

        void dockBoard()
        {
            //Needs to do an event for the activation of the docked state in user states.

            UserState.StateChangeEvent stuff;   //Will be passed to the triggered event....

            stuff.board = Board;

            stuff.leftHand  = GameObject.FindGameObjectWithTag("L_Controller");
            stuff.rightHand = this.gameObject                                 ;

            stuff.position        = this.gameObject.GetComponentInParent<Transform>().position;

            this.gameObject.GetComponentInParent<UserState>().OnDockedStateActivated(stuff);   //Do not feel right about doing it this way....
        }

        // Use this for initialization
        void Start()
        {
            hoverHighlight = this.gameObject.AddComponent<ControllerHoverHighlight>();   //Was supposed to be for highlighting the whiteboards for interaction... Most likely not the right setup for it.

            hoverHighlight.fireHapticsOnHightlight = true;

            hoverHighlight.highLightMaterial = Resources.Load("HoverHighlight", typeof(Material)) as Material;

            hoverHighlight.highLightMaterial = Resources.Load<Material>("SteamVR/InteractionSystem/Materials/HoverHighlight");

            Debug.Log("HoverHighlight: " + hoverHighlight.isActiveAndEnabled);
        }

        // Update is called once per frame
        void Update()
        {
            if (this.gameObject.GetComponent<SteamVR_TrackedController>().gripped)
            {
                dockBoard();
            }
        }
=======
        Board.gameObject.transform.position += translation;
    }

>>>>>>> parent of 307aea1... More Work on states and interaction components.

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject Board;
}
