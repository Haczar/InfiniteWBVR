using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public partial class UserState : MonoBehaviour
    {
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

        public GameObject leftHand ;
        public GameObject rightHand;
        public GameObject fallbackHand;
    }
}
