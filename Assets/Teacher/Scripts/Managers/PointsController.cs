using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsController : MonoBehaviour {
    // static private members
    private static PointsController pointsController;

    // private members
    private float currentPoints;
    private bool gameEnded;
    private float subtTimer;
    private float sessionTimer;

    // public members
    //public Text ScoreText;
    public float SubtAmount;
    public Text SessionTimeText;
    public float SubtTimerThreshold;
    public float SessionTimerThreshold;
    public Image ProgressBarBackground;
    public Image ProgressBarForeground;
    public AudioClip SomeClip;
    private AudioSource source;

    public static void AddPoints(float amount)
    {
        if (pointsController.currentPoints < 100.0f && !pointsController.gameEnded)
        {
            pointsController.source.Play();
            pointsController.currentPoints += amount;
            //pointsController.ScoreText.text = pointsController.currentPoints.ToString();
            pointsController.addBar();
            pointsController.barColor();
        }
    }

    public static void SubtPoints(float amount)
    {
        if (pointsController.currentPoints > 0.00f && !pointsController.gameEnded)
        {
            pointsController.currentPoints -= amount;
            //pointsController.ScoreText.text = pointsController.currentPoints.ToString();
            pointsController.minusBar();
            pointsController.barColor();
        }
    }

    protected void Awake()
    {
        pointsController = this;
        pointsController.currentPoints = 0.00f;
        //pointsController.ScoreText.text = "";
        pointsController.SessionTimeText.text = "";
        pointsController.gameEnded = false;
        pointsController.subtTimer = 0.00f;
        pointsController.sessionTimer = 0.00f;
        //pointsController.SubtAmount = 0.00f;
        source = gameObject.GetComponent<AudioSource>();
    }

    protected void OnDestroy()
    {
        if (pointsController != null)
        {
            pointsController = null;
        }
    }

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        if (!gameEnded)
        {
            subtTimer += Time.deltaTime;
            sessionTimer += Time.deltaTime;
            SessionTimeText.text = Mathf.Round(sessionTimer).ToString();

            if (subtTimer > SubtTimerThreshold)
            {
                SubtPoints(SubtAmount);
                subtTimer = 0.00f;
            }

            if (sessionTimer > SessionTimerThreshold)
            {
                RectTransform rectTransformBackground = ProgressBarBackground.GetComponent<RectTransform>();
                RectTransform rectTransformForeground = ProgressBarForeground.GetComponent<RectTransform>();
                Rect rectBackground = rectTransformBackground.rect;
                Rect rectForeground = rectTransformForeground.rect;
                gameEnded = true;

                if (rectForeground.width > rectBackground.width * 0.20f)
                {
                    Application.LoadLevel("Win Scene");
                }
                else
                {
                    Application.LoadLevel("Lose Scene");
                }
            }
        }
    }

    private void addBar()
    {
        RectTransform rectTransformBackground = ProgressBarBackground.GetComponent<RectTransform>();
        RectTransform rectTransformForeground = ProgressBarForeground.GetComponent<RectTransform>();
        Rect rectBackground = rectTransformBackground.rect;
        Rect rectForeground = rectTransformForeground.rect;

        if (rectForeground.width < rectBackground.width)
        {
            ProgressBarForeground.GetComponent<RectTransform>().sizeDelta = new Vector2(rectBackground.width * (currentPoints/100.00f), rectForeground.height);
        }
    }

    private void minusBar()
    {
        RectTransform rectTransformBackground = ProgressBarBackground.GetComponent<RectTransform>();
        RectTransform rectTransformForeground = ProgressBarForeground.GetComponent<RectTransform>();
        Rect rectBackground = rectTransformBackground.rect;
        Rect rectForeground = rectTransformForeground.rect;

        if (rectForeground.width > 0.00f)
        {
            ProgressBarForeground.GetComponent<RectTransform>().sizeDelta = new Vector2(rectBackground.width * (currentPoints / 100.00f), rectForeground.height);
        }
    }

    private void barColor()
    {

        RectTransform rectTransformBackground = ProgressBarBackground.GetComponent<RectTransform>();
        RectTransform rectTransformForeground = ProgressBarForeground.GetComponent<RectTransform>();
        Rect rectBackground = rectTransformBackground.rect;
        Rect rectForeground = rectTransformForeground.rect;

        if (rectForeground.width > rectBackground.width * 0.20f)
        {
            Color green = new Color();

            green.r = 0.00f;
            green.g = 1.00f;
            green.b = 0.00f;
            green.a = 0.33f;

            ProgressBarForeground.color = green;
        }
        else
        {
            Color red = new Color();

            red.r = 1.00f;
            red.g = 0.00f;
            red.b = 0.00f;
            red.a = 0.33f;

            ProgressBarForeground.color = red;
        }

        Color bg = new Color();

        bg.r = 1.00f;
        bg.g = 1.00f;
        bg.b = 1.00f;
        bg.a = 0.33f;

        ProgressBarBackground.color = bg;
    }
}
