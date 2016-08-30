using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject StartButton;
    public Sprite OnDefault;
    public Sprite OnHover;

    private static MenuController menuController;
    private CardboardHead head;
    private float stareTime;

    protected void Awake()
    {
        menuController = this;
    }

    protected void OnDestroy()
    {
        if (menuController != null)
        {
            menuController = null;
        }
    }

    void Start ()
    {
        head = Camera.main.GetComponent<StereoController>().Head;
        stareTime = 0.00f;
    }
    
    void Update ()
    {
        RaycastHit hit;
        bool isLookedAt = StartButton.GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
        StartButton.GetComponent<Image>().sprite = isLookedAt ? OnHover : OnDefault;

        if (isLookedAt)
        {
            stareTime += Time.deltaTime;

            if (stareTime > 1.00f)
            {
                Handheld.Vibrate();
                Application.LoadLevel("Game Scene");
                stareTime = 0.00f;
            }
        }
        else
        {
            stareTime = 0.00f;
        }
    }
}
