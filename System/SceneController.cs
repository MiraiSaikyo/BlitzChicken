using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    private FadeController m_fade = null;

    public static SceneController Instance
    {
        get;
        private set;
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);

        var obj = new GameObject();
        m_fade = obj.AddComponent<FadeController>();


    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ChangeScene(string i_scene)
    {
        StartCoroutine(ChangeSceneProcess(i_scene));
    }
    public void EndScene()
    {
        StartCoroutine(EndSceneProcess());
    }

    private IEnumerator ChangeSceneProcess(string i_scene)
    {
        m_fade.FadeOut(2.0f, Color.black);
        yield return new WaitWhile(() => m_fade.State == FadeController.EFadeState.Process);
        SceneManager.LoadScene(i_scene);
        m_fade.FadeIn();
        yield return new WaitWhile(() => m_fade.State == FadeController.EFadeState.Process);
    }
    private IEnumerator EndSceneProcess()
    {
        m_fade.FadeOut(2.0f, Color.black);
        yield return new WaitWhile(() => m_fade.State == FadeController.EFadeState.Process);
        Application.Quit();
        yield return new WaitWhile(() => m_fade.State == FadeController.EFadeState.Process);

    }


}
