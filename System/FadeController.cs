using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{


    public enum EFadeState
    {
        In, Out, Process
    }

    private EFadeState _state = default(EFadeState);
    private Image _image = null;
    private Canvas _canvas = null;


    readonly int CANVAS_ORDER = 1000;
    readonly float DEFAULT_TIME = 0.5f;
    readonly Color DEFAULT_COLOR = Color.black;


    public EFadeState State
    {
        get { return _state; }
        private set
        {
            _state = value;
            if (value == EFadeState.In)
            {
                FadeAlpha = 0.0f;
            }
            if (value == EFadeState.Out)
            {
                FadeAlpha = 1.0f;
            }
        }
    }

    public int CanvasOrder
    {
        get { return _canvas.sortingOrder; }
        set { _canvas.sortingOrder = value; }
    }


    private Color FadeColor
    {
        get { return _image.color; }
        set { _image.color = value; }
    }

    private float FadeAlpha
    {
        get { return FadeColor.a; }
        set
        {
            var color = FadeColor;
            color.a = value;
            FadeColor = color;
        }
    }


    private void Awake()
    {
        gameObject.name = "FadeController";
        DontDestroyOnLoad(gameObject);

        // Canvasの作成
        // sortingOrder値に気をつける
        var canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = CANVAS_ORDER;

        // CanvasScalerの作成

        var scaler = gameObject.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = Vector2.one;

        //Fade用のSprite作成
        var obj = new GameObject("FadeImage");
        obj.transform.SetParent(transform, false);
        var image = obj.AddComponent<Image>();

        _image = image;


        State = EFadeState.In;

    }



    public bool FadeOut(float i_time, Color i_color)
    {
        return StartFade(i_time, i_color, EFadeState.Out);
    }

    public bool FadeOut()
    {
        return FadeOut(DEFAULT_TIME, DEFAULT_COLOR);
    }
    public bool FadeOut(float i_time)
    {
        return FadeOut(i_time, DEFAULT_COLOR);
    }
    public bool FadeOut(Color i_color)
    { return FadeOut(DEFAULT_TIME, i_color); }


    public bool FadeIn(float i_time)
    {
        return StartFade(i_time, FadeColor, EFadeState.In);
    }
    public bool FadeIn()
    {
        return FadeIn(DEFAULT_TIME);
    }


    private bool StartFade(float i_time, Color i_color, EFadeState i_fade)
    {
        if (State == EFadeState.Process)
        { return false; }

        if (State == i_fade)
        { return false; }

        FadeColor = i_color;

        i_time = Mathf.Max(i_time, 0.0f);
        if (i_time <= Mathf.Epsilon)
        {
            State = i_fade;
            return true;
        }

        State = EFadeState.Process;
        StartCoroutine(FadeProcess(i_time, i_fade));
        return true;
    }


    IEnumerator FadeProcess(float i_time, EFadeState i_fade)
    {
        float startAlpha = 0.0f;
        float targetAlpha = 0.0f;

        if (i_fade == EFadeState.In)
        {
            startAlpha = 1.0f;
            targetAlpha = 0.0f;
        }
        else
        {
            startAlpha = 0.0f;
            targetAlpha = 1.0f;
        }





        FadeAlpha = startAlpha;

        float startTime = Time.time;
        while (Time.time - startTime <= i_time)
        {
            float timeStep = (Time.time - startTime) / i_time;
            timeStep = Mathf.Clamp01(timeStep);
            FadeAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeStep);
            yield return null;
        }

        State = i_fade;

    }


    
}
