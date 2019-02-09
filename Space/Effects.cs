using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    [SerializeField]
    private Camera secondCamera;

    public void PlayerWarp(Vector3 _target,float _speed) {
		GetComponent<Space> ().ClearAll ();
        secondCamera.GetComponent<SolarSystemCameraControl>().WarpTo(_target,_speed);


    }
	public void PlayerWarpStop() {
		secondCamera.GetComponent<SolarSystemCameraControl>().WarpStop();
		GetComponent<Space> ().UpdateAll ();
	}
}
