using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public partial class UserState : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            FreeStateActive += setFreeState;
            DockedStateActive += setDockedState;
            OverviewStateActive += setOverviewState;

            StateChangeEvent stuff;

            //---------------------------------------------------------Stuff Parameters
            stuff.board = null;

            stuff.leftHand = leftHand;
            stuff.rightHand = rightHand;

            stuff.position = this.gameObject.GetComponentInParent<Transform>().position;
            //-------------------------------------------------------------------------

            OnFreeStateActivated(stuff);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}