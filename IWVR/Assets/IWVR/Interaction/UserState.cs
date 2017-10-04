using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class UserState : MonoBehaviour
    {
        //Events
        public struct StateChangeEvent
        {
            public GameObject board;

            public GameObject leftController ;
            public GameObject rightController;

            public Vector3 position;
        }

        public delegate void StateEventHandler(object sender, StateChangeEvent args);

        public event StateEventHandler FreeStateActive    ;
        public event StateEventHandler DockedStateActive  ;
        public event StateEventHandler OverviewStateActive;

        void setFreeState(object sender, StateChangeEvent args)
        {
            if      (userState == states.docked  ) { this.gameObject.GetComponent<Docked  >().clean(); }
            else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }
                
            this.gameObject.AddComponent<Free>().giveControllers(args.leftController, args.rightController);

            userState = states.free;
        }

        void setDockedState(object sender, StateChangeEvent args)
        {
            if      (userState == states.free    ) { this.gameObject.GetComponent<Free    >().clean(); }
            else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }

            this.gameObject.AddComponent<Docked>().giveDockedBoard(args.board);

            userState = states.docked;
        }

        void setOverviewState(object sender, StateChangeEvent args)
        {
            if      (userState == states.free  ) { Destroy(this.gameObject.GetComponent<Free  >()); }
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
            if (leftController == null)
            {
                leftController = GameObject.FindGameObjectWithTag("L_Controller");

                Debug.Log(leftController);
            }
            if (rightController == null)
            {
                rightController = GameObject.FindGameObjectWithTag("R_Controller");

                Debug.Log(rightController);
            }
        }

        // Use this for initialization
        void Start()
        {
            FreeStateActive     += setFreeState    ;
            DockedStateActive   += setDockedState  ;
            OverviewStateActive += setOverviewState;

            StateChangeEvent stuff;

            //---------------------------------------------------------Stuff Parameters
            stuff.board = null;

            stuff.leftController  = leftController ;
            stuff.rightController = rightController;

            stuff.position = this.gameObject.GetComponentInParent<Transform>().position;
            //-------------------------------------------------------------------------

            OnFreeStateActivated(stuff);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public enum states
        {
            free    ,
            docked  ,
            overview,
        }

        public enum HandController
        {
            left, right
        }

        public states userState;

        public HandController locoController     = HandController.left ;
        public HandController interactController = HandController.right;

        public GameObject leftController ;
        public GameObject rightController;
    }
}