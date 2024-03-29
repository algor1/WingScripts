﻿using UnityEngine;
using System.Collections;


public class MissileLauncher : MonoBehaviour {

	[SerializeField]
	GameObject player;
    public GameObject missilePrefab;
    private GameObject target;


    void Start()
    {
        missilePrefab = Resources.Load("prefabs/Missile") as GameObject;
    }

    public void Fire(GameObject _target)
    {
        target = _target;
        //		GameObject target = GameObject.Find ("Cube");
        GameObject missile = (GameObject)Instantiate(missilePrefab, transform.position, transform.rotation);
        missile.GetComponent<Missile>().target = target;
    }
}