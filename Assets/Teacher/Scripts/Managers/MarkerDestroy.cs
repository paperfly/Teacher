using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarkerDestroy : MonoBehaviour {

    static private List<MarkerDestroy> markerDestroys;

    private ParticleSystem explosionPS;
    static public MarkerDestroy Spawn(Vector3 location)
    {
        foreach (MarkerDestroy markerDestroy in markerDestroys)
        {
            if (markerDestroy.gameObject.activeSelf == false)
            {
                markerDestroy.transform.position = location;
                markerDestroy.gameObject.SetActive(true);

                return markerDestroy;
            }
        }
        return null;
    }

	void Start () {
        gameObject.SetActive(false);
	}

    void Awake()
    {
        if (markerDestroys == null)
        {
            markerDestroys = new List<MarkerDestroy>();
        }
        markerDestroys.Add(this);

        explosionPS = gameObject.GetComponent<ParticleSystem>();
    }

    protected void OnDestroy()
    {
        markerDestroys.Remove(this);

        if (markerDestroys.Count == 0)
        {
            markerDestroys = null;
        }
    }
	
	void Update () {
        if (explosionPS.IsAlive() == false)
        {
            gameObject.SetActive(false);
        }
	}
}
