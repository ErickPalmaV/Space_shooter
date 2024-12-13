using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    [SerializeField] Image image;
    IEnumerator ScreenFadeOut()
    {
        Color color = image.color;
        for (float alpha = 1; alpha >=0; alpha -= 0.02f)
        {
            color.a = alpha;
            image.color = color;
            yield return new WaitForSeconds(0.005f);
        }
        color.a = 0;
        image.color = color;
        gameObject.SetActive(false);
    }
    IEnumerator ScreenFadeIn()
    {
        Color color = image.color;
        for (float alpha = 0; alpha <=1; alpha += 0.01f)
        {
            color.a = alpha;
            image.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        color.a = 1;
        image.color = color;
    }
    public void FadeOutScreen()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        StartCoroutine(ScreenFadeOut());
    }
    public void FadeInScreen()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        StartCoroutine(ScreenFadeIn());
    }
    public void FadeInOutScreen(GameObject target)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        StartCoroutine(ScreenFadeInOut(target));
    }
    IEnumerator ScreenFadeInOut(GameObject target)
    {
        Color color = image.color;
        for (float alpha = 0; alpha <=1; alpha += 0.02f)
        {
            color.a = alpha;
            image.color = color;
            yield return new WaitForSeconds(0.005f);
        }
        color.a = 1;
        target.SetActive(false);
        image.color = color; 
        for (float alpha = 1; alpha >=0; alpha -= 0.02f)
        {
            color.a = alpha;
            image.color = color;
            yield return new WaitForSeconds(0.005f);
        }
        color.a = 0;
        image.color = color;
        gameObject.SetActive(false);
    }
}
