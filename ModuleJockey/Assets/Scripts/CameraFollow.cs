using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	Transform camtransform;

	public float smoothSpeed;
    private void Start()
    {
     
        target = app.playermanager.player.transform;
        camtransform = Camera.main.transform;
      
       
    }

    void LateUpdate(){

        camtransform.position = new Vector3(target.position.x, camtransform.position.y, camtransform.position.z);


	}
}
