using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    public SpriteRenderer sprite { get { return GetComponent<SpriteRenderer> ( ); } }
    public float sensitivity;

	void Update () {
        if (PauseMenu.isOpen)
            return;

        transform.position += Vector3.right * Input.GetAxis ( "Mouse X" ) * sensitivity;

        Vector3 depenetrationDir;
        if ( Camera.main.GetComponent<CameraHelper> ( ).IsOutside ( sprite.bounds, out depenetrationDir ) )
            transform.position += depenetrationDir;
    }
}
