using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndController : MonoBehaviour
{
    public GameObject ReplayButton;
    public Sprite ReplayOnDefault;
    public Sprite ReplayOnHover;
    public GameObject QuitButton;
    public Sprite QuitOnDefault;
    public Sprite QuitOnHover;

    private static EndController endController;
    private CardboardHead head;
    private float stareTime;

    protected void Awake()
    {
        endController = this;
    }

    protected void OnDestroy()
    {
        if (endController != null)
        {
            endController = null;
        }
    }

    void Start () {
        head = Camera.main.GetComponent<StereoController>().Head;
        stareTime = 0.00f;
    }
    
    void Update () {
        RaycastHit hit;
        bool replayIsLookedAt = ReplayButton.GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        bool quitIsLookedAt = QuitButton.GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        ReplayButton.GetComponent<Image>().sprite = replayIsLookedAt ? ReplayOnHover : ReplayOnDefault;
        QuitButton.GetComponent<Image>().sprite = quitIsLookedAt ? QuitOnHover : QuitOnDefault;

        if (replayIsLookedAt || quitIsLookedAt)
        {
            stareTime += Time.deltaTime;

            if (stareTime > 1.00f) {
                Handheld.Vibrate();

                if (replayIsLookedAt) {
                    Application.LoadLevel("Menu Scene");
                } 
                else if (quitIsLookedAt)
                {
                    Application.Quit();
                }

                stareTime = 0.00f;
            }
        }
        else
        {
            stareTime = 0.00f;
        }
    }
}
