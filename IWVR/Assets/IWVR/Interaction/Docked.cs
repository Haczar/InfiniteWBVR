using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class Docked : MonoBehaviour
    {
        public void giveDockedBoard(GameObject dockedBoard)
        {
            this.dockedBoard = dockedBoard;
        }

        public void clean()  //Destory's state related componenets
        {
            Destroy(this.gameObject.GetComponent<Docked>());
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private GameObject dockedBoard;
    }
}

