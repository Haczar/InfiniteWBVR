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
            this.leftController  = leftController ;
            this.rightController = rightController;
        }

        public void clean()  //Destory's state related componenets
        {
            Destroy(leftController .GetComponent<SteamVR_TeleporterModified>());
            Destroy(rightController.GetComponent<BoardManipulation         >());
            Destroy(this.gameObject.GetComponent<Free                      >());
        }

        // Use this for initialization
        void Start()
        {
            leftController.AddComponent<SteamVR_TeleporterModified>().teleportOnClick = true;

            rightController.AddComponent<BoardManipulation>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private GameObject leftController ;
        private GameObject rightController;
    }
}