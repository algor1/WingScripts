
using UnityEngine;
using SpaceObjects;

public class SOParametres : MonoBehaviour {

	public SpaceObject thisServerObject;
	[SerializeField]
	private GameObject spaceManager;

	public void Init (SpaceObject thisSO_, GameObject sM)
		{
		thisServerObject= new SpaceObject(thisSO_);
		spaceManager = sM;

	}

	void Update(){
		transform.position = thisServerObject.Position-spaceManager.GetComponent<Space>().GetZeroPoint();

	}
}
