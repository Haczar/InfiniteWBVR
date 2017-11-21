using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
   public ColorManager cm;
    public ColorManager dick;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = cm.color;
		
	}
}
