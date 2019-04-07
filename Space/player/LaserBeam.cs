using UnityEngine;
using System.Collections;
using SpaceObjects;

[RequireComponent(typeof(LineRenderer))]

public class LaserBeam : MonoBehaviour {

	[SerializeField]
	GameObject player;
	//laser
	LineRenderer laser_shot;
	public Material laserMaterial;
	private bool laser_enabled;
	private GameObject target;
	// Use this for initialization
	void Start () {
		initLaser ();
		stoplaser ();

	}

	void Update () {
		if (laser_shot.enabled  && target!=null) {
			//Debug.Log("from "+ transform.position+ "!!!!!!!!pew to " + target);
			if (target != null) {
				laser_shot.SetPosition (0, transform.position);
				laser_shot.SetPosition (1, target.GetComponent<Transform> ().position);
			}
		}
		if (target == null) {
            stoplaser();
		}

	}


	void initLaser()
	{
		laser_shot = GetComponent<LineRenderer>();
		laser_shot.positionCount=2;
		laser_shot.GetComponent<Renderer>().material = laserMaterial;
		laser_shot.startWidth = 0.1f;
		laser_shot.endWidth=0.03f;
	}




	void stoplaser(){
		laser_shot.enabled = false;

    }


    public void StartFire (GameObject _target)
	{
		target = _target;
        //		Debug.Log ("====laser pew to " + target.p.SO.visibleName);
        //		Debug.Log( laser_shot.GetPosition(0)+ "    "  +  laser_shot.GetPosition(1));
        //Debug.Log("------------------  FIRE ---------------------------");

        laser_shot.enabled = true;

	}
	public void StopFire ()
	{
		laser_shot.enabled = false;
        //Debug.Log("------------------  Stop FIRE ---------------------------");

    }
}