/*Developed and provided by SpeedTutor - www.speed-tutor.com*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashFade : MonoBehaviour {
    public Image splashImage;
    public string loadLevel;

    IEnumerator Start() {
        splashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadSceneAsync(loadLevel);
        //SceneManager.LoadScene(loadLevel);
    }

    void FadeIn() {
        splashImage.CrossFadeAlpha(1.0f, 2.5f, false);
    }
    /*
    public void StartLoading() {
        StartCoroutine("load");
    }

    IEnumerator load() {
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        async = Application.LoadLevelAsync(levelName);
        async.allowSceneActivation = false;
        yield return async;
    }

    public void ActivateScene() {
        async.allowSceneActivation = true;
    }
    */
}
