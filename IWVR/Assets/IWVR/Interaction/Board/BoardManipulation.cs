using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace IWVR
{
    public class BoardManipulation : MonoBehaviour
    {
        //void dockBoard()
        //{
        //    UserState.PlayerStuff stuff = new UserState.PlayerStuff();   //Will be passed to the triggered event....

        //    stuff.leftHand  = GameObject.FindGameObjectWithTag("L_Controller");
        //    stuff.rightHand = this.gameObject                                 ;

        //    //this.gameObject.GetComponentInParent<UserState>().OnDockedStateActivated(stuff);   //Do not feel right about doing it this way....
        //}

        void createBoard()
        {
            boardNum++;  newBoard = Instantiate(board);

            newBoard.transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z + 1.5f);
            newBoard.transform.rotation = hand.transform.rotation * Quaternion.AngleAxis(0, Vector3.right);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
            {
                createBoard();
            }
        }


        public Hand hand;

        public GameObject board;

        private int boardNum = 0;

        private string boardName = "Board ";

        private GameObject newBoard;

        private ControllerHoverHighlight hoverHighlight;

        private SteamVR_Controller.Device device;
    }
}