using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Fade Effect for a smooth transition between start and game scene.
    /// </summary>
    public class FadeScreen : MonoBehaviour
    {
        public bool fadeOnStart = true;
        public float fadeDuration = 2;
        public Color fadeColor;
        public AnimationCurve fadeCurve;
        public string colorPropertyName = "_Color";
        private Renderer rend;

        // Get renderer component and do fade in effect when opening the application. 
        private void Start()
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;

            if (fadeOnStart)
                FadeIn();
        }

        // Call the Fade function from opaque to transparent.
        public void FadeIn()
        {
            Fade(1, 0);
        }

        // Call the Fade function from transparent to opaque.
        public void FadeOut()
        {
            Fade(0, 1);
        }

        // Start the fade transition
        public void Fade(float alphaIn, float alphaOut)
        {
            StartCoroutine(FadeRoutine(alphaIn, alphaOut));
        }

        // Change transparency level over time.
        public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
        {
            rend.enabled = true;

            float timer = 0;
            while (timer <= fadeDuration)
            {
                Color newColor = fadeColor;
                newColor.a = Mathf.Lerp(alphaIn, alphaOut, fadeCurve.Evaluate(timer / fadeDuration));

                // Update material color with new alpha value.
                rend.material.SetColor(colorPropertyName, newColor);

                timer += Time.deltaTime;
                yield return null;
            }

            Color newColor2 = fadeColor;
            newColor2.a = alphaOut;
            rend.material.SetColor(colorPropertyName, newColor2);

            if (alphaOut == 0)
                rend.enabled = false;
        }
    }

}
