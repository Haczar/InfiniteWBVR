using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class UserState : MonoBehaviour
    {
        public struct StateChangeEvent
        {
            public GameObject dockedBoard;

            public Vector3 position;
        }

        public delegate void StateEventHandler(object sender, StateChangeEvent args);

        public event StateEventHandler FreeStateActive    ;
        public event StateEventHandler DockedStateActive  ;
        public event StateEventHandler OverviewStateActive;

        protected virtual void OnFreeStateActive(StateChangeEvent args)
        {
            StateEventHandler handler = FreeStateActive;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        void ActivateFreeState(object sender, StateChangeEvent args)
        {
            this.gameObject.AddComponent<Free>();
        }


        // Use this for initialization
        void Start()
        {
            FreeStateActive += ActivateFreeState;

            StateChangeEvent something = new StateChangeEvent();

            ActivateFreeState(this, something);
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

        public states userState;
    }
}