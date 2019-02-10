﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Indicators : MonoBehaviour
{
    [SerializeField]
    private GameObject playerobj;
    [SerializeField]
    private GameObject spaceManager;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private GameObject indicatorprefab;
    [SerializeField]
    private Points wp;

    //	private Vector3 target_tmp;
    private List<GameObject> wpList;
    private List<GameObject> indicatorsList_wp;
    private List<GameObject> indicatorsList_sh;
    private List<GameObject> indicatorsList_so;

    private bool initialaized;


    //private Dictionary<int,GameObject> nearestShips;


    public void Init(GameObject _player)
    {
        playerobj = spaceManager.GetComponent<Space>().Player;
        CreateIndicators_sh();
        CreateIndicators_so ();
        //CreateIndicators_wp ();
        //wpList= spaceManager.GetComponent<Space>().wpList;
		initialaized=true;

    }
    void Update()
    {
        if (initialaized)
        {
            //UpdateIndicators_wp ();
            UpdateIndicators_sh();
            UpdateIndicators_so ();
            TouchEvents();
        }
    }
    private void CreateIndicators_wp()
    {
        indicatorsList_wp = new List<GameObject>();
        for (int i = 0; i < wpList.Count; i++)

        {
            GameObject indicator_wp = (GameObject)Instantiate(indicatorprefab);
            indicator_wp.GetComponent<IndicatorLink>().linkedObj = wpList[i];
            indicator_wp.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            indicatorsList_wp.Add(indicator_wp);
        }
    }
    private void UpdateIndicators_wp()
    {
        for (int i = 0; i < indicatorsList_wp.Count; i++)
        {

            Vector3 screenPos = mainCam.WorldToScreenPoint(indicatorsList_wp[i].GetComponent<IndicatorLink>().linkedObj.transform.position);
            if (screenPos.z > 0)
            {
                indicatorsList_wp[i].GetComponent<RectTransform>().position = screenPos.x * Vector3.right + screenPos.y * Vector3.up;
            }
        }
    }
    private void CreateIndicators_sh()
    {
        indicatorsList_sh = new List<GameObject>();
        //		Debug.Log(indicatorsList_sh);


    }

    public void AddIndicator_sh(GameObject linkObj)
    {
        GameObject indicator_sh = (GameObject)Instantiate(indicatorprefab);
        indicator_sh.GetComponent<IndicatorLink>().linkedObj = linkObj;
        //indicator_sh.GetComponent<IndicatorLink>().player=playerobj;
        indicator_sh.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        indicatorsList_sh.Add(indicator_sh);




    }
    public void DeleteIndicator_sh(int ship_id)
    {
        for (int i = 0; i < indicatorsList_sh.Count; i++)
        {
            if (indicatorsList_sh[i].GetComponent<IndicatorLink>().linkedObj.GetComponent<ShipMotor>().thisShip.p.Id == ship_id)
            {
                Debug.Log("Delete Ship  " + indicatorsList_sh[i].GetComponent<IndicatorLink>().linkedObj.GetComponent<ShipMotor>().thisShip.p.VisibleName);
                GameObject indicator_sh = indicatorsList_sh[i];
                indicatorsList_sh.RemoveAt(i);
                Destroy(indicator_sh);
                break;

            }
        }
    }


    private void UpdateIndicators_sh()
    {
        //		Debug.Log ("SH   "+indicatorsList_sh.Count);

        for (int i = 0; i < indicatorsList_sh.Count; i++)
        {

            Vector3 screenPos = mainCam.WorldToScreenPoint(indicatorsList_sh[i].GetComponent<IndicatorLink>().linkedObj.transform.position);
            if (screenPos.z > 0)
            {
                indicatorsList_sh[i].GetComponent<RectTransform>().position = screenPos.x * Vector3.right + screenPos.y * Vector3.up;
            }
        }


    }
    //---------------------------------- SO ----------------------------
    private void CreateIndicators_so()
    {
        indicatorsList_so = new List<GameObject>();
        //		Debug.Log(indicatorsList_sh);


    }

    public void AddIndicator_so(GameObject linkObj)
    {
        GameObject indicator_so = (GameObject)Instantiate(indicatorprefab);
        indicator_so.GetComponent<IndicatorLink>().linkedObj = linkObj;
        indicator_so.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        indicatorsList_so.Add(indicator_so);




    }
    public void DeleteIndicator_so(int so_id)
    {
        for (int i = 0; i < indicatorsList_so.Count; i++)
        {
            if (indicatorsList_so[i].GetComponent<IndicatorLink>().linkedObj.GetComponent<SOParametres>().thisServerObject.Id == so_id)
            {
                Debug.Log("Delete SO  " + indicatorsList_so[i].GetComponent<IndicatorLink>().linkedObj.GetComponent<SOParametres>().thisServerObject.VisibleName);
                GameObject indicator_sh = indicatorsList_so[i];
                indicatorsList_so.RemoveAt(i);
                Destroy(indicator_sh);
                break;

            }
        }
    }


    private void UpdateIndicators_so()
    {
        //		Debug.Log ("SO   "+indicatorsList_so.Count);

        for (int i = 0; i < indicatorsList_so.Count; i++)
        {


            Vector3 screenPos = mainCam.WorldToScreenPoint(indicatorsList_so[i].GetComponent<IndicatorLink>().linkedObj.transform.position);
            if (screenPos.z > 0)
            {
                indicatorsList_so[i].GetComponent<RectTransform>().position = screenPos.x * Vector3.right + screenPos.y * Vector3.up;
            }
        }


    }
    //---------------------------------- SO  END  ----------------------------





    void TouchEvents()
    {
        GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
        //Create the PointerEventData with null for the EventSystem
        PointerEventData ped = new PointerEventData(null);
        //Set required parameters, in this case, mouse position

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //				print ("touch");
                ped.position = Input.GetTouch(i).position;
                //Create list to receive all results
                List<RaycastResult> results = new List<RaycastResult>();
                //Raycast it
                gr.Raycast(ped, results);
                if (results.Count > 0)
                {
                    //					print (results [0]);
                    if (indicatorsList_sh.Contains(results[0].gameObject))
                    {
                        Debug.Log(results[0].gameObject.GetComponent<IndicatorLink>().linkedObj);//.GetComponent<ShipMotor>().thisShip.p.visibleName);
                        spaceManager.GetComponent<CommandManager>().SendUserCommand(SpaceObjects.Command.SetTarget, results[0].gameObject.GetComponent<IndicatorLink>().linkedObj);
                    }
                    //if (indicatorsList_wp.Contains (results [0].gameObject)||indicatorsList_so.Contains (results [0].gameObject)) {
                    //Debug.Log (results [0].gameObject.GetComponent<IndicatorLink> ().linkedObj);//.GetComponent<ShipMotor>().thisShip.p.visibleName);

                    //Debug.Log(" playerobj.GetComponent<ShipMotor> ().SetTarget(results [0].gameObject.GetComponent<IndicatorLink> ().linkedObj.GetComponent<SOParametres>().thisServerObject);");
                    //}
                }
            }
        }

    }

}