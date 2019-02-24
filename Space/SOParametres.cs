
using UnityEngine;
using SpaceObjects;

public class SOParametres : MonoBehaviour {

	public SpaceObject thisServerObject;
	[SerializeField]
	private GameObject spaceManager;

	public void Init (SpaceObject thisSO, GameObject sM)
		{
		thisServerObject= new SpaceObject(thisSO);
		spaceManager = sM;

	}

	void Update(){
		transform.position = thisServerObject.Position-spaceManager.GetComponent<Space>().GetZeroPoint();

	}
}
