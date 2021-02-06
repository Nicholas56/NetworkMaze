using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow360 : MonoBehaviour {

	static public Transform player;
	public float distance = 10;
	public float height = 10;
	public Vector3 lookOffset = new Vector3(0,1,0);
	public float cameraSpeed = 10;
	public float rotSpeed = 10;

	void Update () 
	{
		if(player)
		{
			transform.position = player.position + new Vector3(0, height, -distance);
			transform.LookAt(player);
		}
	}
}
