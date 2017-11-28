using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public partial class UserState : MonoBehaviour
    {
        public struct PlayerStuff
        {
            public GameObject hoveredObjectLeft;
            public GameObject hoveredObjectRight;

            public GameObject leftHand;
            public GameObject rightHand;

            public GameObject attachedObjectLeft;
            public GameObject attachedObjectRight;
        }

        public delegate void StateEventHandler(object sender, PlayerStuff args);

        public event StateEventHandler GeneralEvent;
        public event StateEventHandler ObjectAttached;
        public event StateEventHandler ObjectHovered;

        

        public virtual void OnGeneralEvent(PlayerStuff stuff)
        {
            if (GeneralEvent != null)
                GeneralEvent(this, stuff);
        }

        private void startup(object sender, PlayerStuff stuff)
        {
            this.gameObject.AddComponent<Free>().giveStuff(stuff);
        }   
    }
}

//Events
//public struct StateChangeEvent
//{
//    public GameObject board;

//    public GameObject leftHand;
//    public GameObject rightHand;

//    public Vector3 position;   //Do not know if we need this yet.
//}

//public event StateEventHandler FreeStateActive;
//public event StateEventHandler DockedStateActive;
//public event StateEventHandler OverviewStateActive;

//void setFreeState(object sender, StuffSender args)
//{
//    if (userState == states.docked) { this.gameObject.GetComponent<Docked>().clean(); }
//    else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }

//    this.gameObject.AddComponent<Free>().giveControllers(args.leftHand, args.rightHand);

//    userState = states.free;
//}

//void setDockedState(object sender, StuffSender args)
//{
//    if (userState == states.free) { this.gameObject.GetComponent<Free>().clean(); }
//    else if (userState == states.overview) { this.gameObject.GetComponent<Overview>().clean(); }

//    this.gameObject.AddComponent<Docked>().giveStuff(args.hoveredObject, args.leftHand, args.rightHand);

//    userState = states.docked;
//}

//void setOverviewState(object sender, StuffSender args)
//{
//    if (userState == states.free) { Destroy(this.gameObject.GetComponent<Free>()); }
//    else if (userState == states.docked) { Destroy(this.gameObject.GetComponent<Docked>()); }

//    this.gameObject.AddComponent<Overview>();

//    userState = states.overview;
//}


//public virtual void OnFreeStateActivated(StateChangeEvent args)
//{
//    if (FreeStateActive != null)
//        FreeStateActive(this, args);
//}

//public virtual void OnDockedStateActivated(StateChangeEvent args)
//{
//    if (DockedStateActive != null)
//        DockedStateActive(this, args);
//}

//public virtual void OnOverviewStateActivated(StateChangeEvent args)
//{
//    if (OverviewStateActive != null)
//        OverviewStateActive(this, args);
//}