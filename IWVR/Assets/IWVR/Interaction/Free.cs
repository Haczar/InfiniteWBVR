using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class Free : MonoBehaviour
    {
        //Setup
        public void giveControllers(GameObject leftController, GameObject rightController)
        {
            this.leftHand  = leftController ;
            this.righthand = rightController;
        }

        public void clean()  //Destory's state related componenets
        {
            Destroy(leftHand .GetComponent<SteamVR_TeleporterModified>());
            Destroy(righthand.GetComponent<BoardManipulation         >());
            Destroy(this.gameObject.GetComponent<Free                >());
        }

        // Use this for initialization
        void Start()
        {
            righthand.AddComponent<BoardManipulation>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private GameObject leftHand ;
        private GameObject righthand;
    }
}