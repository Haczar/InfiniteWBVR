using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overview : MonoBehaviour
{

    public void clean()  //Destory's state related componenets
    {
        Destroy(this.gameObject.GetComponent<Overview>());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
