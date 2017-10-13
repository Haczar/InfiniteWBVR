using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWVR
{
    public class Free : MonoBehaviour
    {
        //Setup
        public void giveStuff(UserState.PlayerStuff stuff)
        {
            this.stuff = stuff;
        }

        public void clean()  //Destory's state related componenets
        {
            Destroy(stuff.rightHand.GetComponent<BoardManipulation         >());
            Destroy(this.gameObject.GetComponent<Free                      >());
        }

        // Use this for initialization
        void Start()
        {
            stuff.rightHand.AddComponent<BoardManipulation>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        UserState.PlayerStuff stuff;
    }
}