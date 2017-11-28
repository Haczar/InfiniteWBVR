using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public partial class UserState : MonoBehaviour
    {
        //Controller Setup
        void FindHands()
        {
            if (leftHand == null)
            {
                leftHand = GameObject.FindGameObjectWithTag("L_Controller");

                Debug.Log(leftHand);
            }
            if (rightHand == null)
            {
                rightHand = GameObject.FindGameObjectWithTag("R_Controller");

                Debug.Log(rightHand);
            }
        }

        void updateStuff()
        {
            ////check to see if hand has an object attached to it.
            //if (leftHand.attachedObj != null) { stuff.attachedObjectLeft = leftHand.attachedObj; }
            //if (rightHand.attachedObj != null) { stuff.attachedObjectRight = rightHand.attatchedObj; }

            ////Check to see if hand is hovering over a object.
            //if (leftHand.hoverObj != null) { stuff.hoveredObjectLeft = leftHand.hoverObj; }
            //if (rightHand.hoverObj != null) { stuff.hoveredObjectRight = rightHand.hoverObj; }
        }

    }
}
