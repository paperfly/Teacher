using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerController : MonoBehaviour
{

    static private List<MarkerController> markerControllers;

    private CardboardHead head;
    public float stareTimer = 0.0f;

    void Start()
    {
        head = Camera.main.GetComponent<StereoController>().Head;

        gameObject.SetActive(true);
    }

    void Update()
    {
        RaycastHit hit;
        bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        //GetComponent<Renderer>().material.color = isLookedAt ?  : Color.blue;
        if (isLookedAt)
        {
            GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);
            stareTimer += Time.deltaTime;
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_TintColor", new Color(1.0f, 0.04f, 0.03f, 0.02f));
            stareTimer = 0;
        }

        if (stareTimer >= 1.0f)
        {
            stareTimer = 0;
            StudentController.checkMarker(true);
        }
    }

    protected void Awake()
    {
        if (markerControllers == null)
        {
            markerControllers = new List<MarkerController>();
        }
        markerControllers.Add(this);
    }

    static public MarkerController Teleport(Vector3 location)
    {

        foreach (MarkerController markerController in markerControllers)
        {
            markerController.transform.position = location;

            return markerController;
        }

        return null;
    }

    protected void OnDestroy()
    {
        if (markerControllers != null)
        {
            markerControllers = null;
        }
    }
}
