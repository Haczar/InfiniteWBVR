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
            //---------------------------------------------------------Stuff Parameters
            stuff.leftHand  = leftHand ;
            stuff.rightHand = rightHand;
            //-------------------------------------------------------------------------

            GeneralEvent += startup;

            OnGeneralEvent(stuff);

            GeneralEvent -= startup;
        }

        // Update is called once per frame
        void Update()
        {
            updateStuff();
        }
    }
}