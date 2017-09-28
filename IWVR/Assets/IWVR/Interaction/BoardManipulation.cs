using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManipulation : MonoBehaviour
{
    void moveBoard(Vector3 translation)
    {
        Board.gameObject.transform.position += translation;
    }


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject Board;
}
