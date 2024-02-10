using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TN
{
    /// <summary>
    /// Manage transition between start and game scene.
    /// </summary>
    public class SceneTransitionManager : MonoBehaviour
    {
        public FadeScreen fadeScreen;
        public static SceneTransitionManager instance;

        // Check that no other SceneTransitionManager instance exists and if so delete it and initialize this one.
        private void Awake()
        {
            if (instance && instance != this)
                Destroy(instance);

            instance = this;
        }

        // Start transition to game scene. 
        public void GoToScene(int sceneIndex)
        {
            StartCoroutine(GoToSceneRoutine(sceneIndex));
        }

        // Start fade transition and wait until transition effect is completed. Then activate other scene.
        IEnumerator GoToSceneRoutine(int sceneIndex)
        {
            fadeScreen.FadeOut();
            //Launch the new scene
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            float timer = 0;
            while (timer <= fadeScreen.fadeDuration && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            operation.allowSceneActivation = true;
        }
    }

}

