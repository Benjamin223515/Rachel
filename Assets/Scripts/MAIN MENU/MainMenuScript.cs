using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject a;
    public Text progress;

    public float fadeSpeed = 1f;

    void Start()
    {
        StartCoroutine(LoadGame());
        progress.enabled = false;
    }


    void Update()
    {
        progress.color = new Color(255,255,255, 0.5f + (Mathf.Sin(Time.time * fadeSpeed) * 0.25f));
    }

    public void Play()
    {
        if (operation.progress >= 0.89f) play = true;
        else
        {
            StartCoroutine(change());
            progress.text = "GAME LOADING";
        }
    }

    public IEnumerator change()
    {
        yield return new WaitForSeconds(3);
        changeText = false;
    }

    bool changeText = false;

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    bool play = false;

    AsyncOperation operation;

    public IEnumerator LoadGame()
    {
        yield return null;

        operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = play;

        while(!operation.isDone)
        {
            if(!changeText)
            {
                if(operation.progress >= 0.89f)
                {
                    progress.enabled = false;
                }
                else
                {
                    progress.enabled = true;
                    progress.text = "LOADING GAME - " + (operation.progress * 100) + "%";
                }
            }
            
            operation.allowSceneActivation = play;
            yield return null;
        }
    }
}
