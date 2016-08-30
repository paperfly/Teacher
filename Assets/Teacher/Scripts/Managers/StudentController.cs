using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StudentController : MonoBehaviour {
    static private List<StudentController> studentControllers;

    public float maxTime = 5.0f;
    public float markedTimer = 0.0f;
    public static bool markChecked = false;
    public Vector3 newPost;
    public Vector3 prevPost;
    public int rnd;
    void Start()
    {
        rnd = Random.Range(0, studentControllers.Count);
        newPost = studentControllers[rnd].transform.position;
        newPost += Vector3.up * 1.9f;
        prevPost = newPost;
        MarkerController.Teleport(newPost);
    }

    protected void Awake()
    {
        if (studentControllers == null)
        {
            studentControllers = new List<StudentController>();
        }
        studentControllers.Add(this);
    }

    protected void Update()
    {
        markedTimer += Time.deltaTime;
        if (markedTimer >= maxTime || markChecked == true)
        {
            //if (markChecked == true)
            //{
            //    MarkerDestroy.Spawn(newPost);
            //}
            rnd = Random.Range(0, studentControllers.Count);
            if (newPost == prevPost)
            {
                rnd = Random.Range(0, studentControllers.Count);
            }
            newPost = studentControllers[rnd].transform.position;
            newPost += Vector3.up * 1.9f;
            prevPost = newPost;
            MarkerController.Teleport(newPost);
            foreach (StudentController studentController in studentControllers)
            {
                studentController.markedTimer = 0.0f;
                markChecked = false;
            }
        }
    }

    static public void checkMarker(bool markerChecked)
    {
        markChecked = markerChecked;
        Handheld.Vibrate();
        PointsController.AddPoints(5.00f);
    }

    public void OnDestroy()
    {
        if (studentControllers != null)
        {
            studentControllers = null;
        }
    }


}
