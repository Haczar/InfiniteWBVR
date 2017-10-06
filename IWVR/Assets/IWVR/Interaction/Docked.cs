using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class Docked : MonoBehaviour
    {
<<<<<<< HEAD
        public void giveStuff(GameObject dockedBoard, GameObject leftController, GameObject rightController)
        {
            this.dockedBoard = dockedBoard;

            this.leftController = leftController;
            this.rightController = rightController;
        }

        public void clean()  //Destory's state related componenets
        {
            Destroy(this.gameObject.GetComponent<Docked>());
        }
=======
>>>>>>> parent of 307aea1... More Work on states and interaction components.

        // Use this for initialization
        void Start()
        {
            leftController.AddComponent<PanningLoco>().active = true;
            rightController.AddComponent<LineDraw>();
        }

        // Update is called once per frame
        void Update()
        {
            if (leftController.GetComponent<SteamVR_TrackedController>().gripped)
            {
                UserState.StateChangeEvent stuff;

                stuff.board = dockedBoard;

                stuff.leftHand  = leftController;
                stuff.rightHand = rightController;

                stuff.position = position;

                this.gameObject.GetComponentInParent<UserState>().OnFreeStateActivated(stuff);
            }
        }

        private GameObject dockedBoard;

        private GameObject leftController;
        private GameObject rightController;

        private Vector3 position;
    }
}