using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public partial class UserState : MonoBehaviour
    {
        //Events
        public struct StateChangeEvent
        {
            public GameObject board;

            public GameObject leftHand;
            public GameObject rightHand;

            public Vector3 position;   //Do not know if we need this yet.
        }

        public delegate void StateEventHandler(object sender, StateChangeEvent args);

        public event StateEventHandler FreeStateActive;
        public event StateEventHandler DockedStateActive;
        public event StateEventHandler OverviewStateActive;

        void setFreeState(object sender, StateChangeEvent args)
        {
            if      (userState == states.docked  ) { this.gameObject.GetComponent<Docked  >().clean(); }
            else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }

            this.gameObject.AddComponent<Free>().giveControllers(args.leftHand, args.rightHand);

            userState = states.free;
        }

        void setDockedState(object sender, StateChangeEvent args)
        {
            if (userState == states.free) { this.gameObject.GetComponent<Free>().clean(); }
            else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }

            this.gameObject.AddComponent<Docked>().giveStuff(args.board, args.leftHand, args.rightHand);

            userState = states.docked;
        }

        void setOverviewState(object sender, StateChangeEvent args)
        {
            if (userState == states.free) { Destroy(this.gameObject.GetComponent<Free>()); }
            else if (userState == states.docked) { Destroy(this.gameObject.GetComponent<Docked>()); }

            this.gameObject.AddComponent<Overview>();

            userState = states.overview;
        }

        public virtual void OnFreeStateActivated(StateChangeEvent args)
        {
            if (FreeStateActive != null)
                FreeStateActive(this, args);
        }

        public virtual void OnDockedStateActivated(StateChangeEvent args)
        {
            if (DockedStateActive != null)
                DockedStateActive(this, args);
        }

        public virtual void OnOverviewStateActivated(StateChangeEvent args)
        {
            if (OverviewStateActive != null)
                OverviewStateActive(this, args);
        }

        //Controller Setup
        void checkControllers()
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
    }
}