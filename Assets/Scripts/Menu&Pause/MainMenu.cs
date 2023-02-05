using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public DialogManager dialogManager;
    public GameObject optionsmenu;
    public GameObject menubuttons;
    public GameObject backbutton;
    public GameObject credits;
    public Slider soundoption;
    public Slider musicoption;
    public Image Dark;
    public void Play(string mode)
    {
        StartCoroutine(StartCo(mode));
    }


    
    IEnumerator StartCo(string mode)
    {
        
        if (mode == "endless")
        {
            Dark.gameObject.SetActive(true);
            StartCoroutine(FadeIn(Dark, 2));
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("Endless");
        }
        else
        {
            menubuttons.SetActive(false);
            dialogManager.GetDialogs(dialogManager.FindPath("first"));
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        menubuttons.SetActive(false);
        backbutton.SetActive(true);
        credits.SetActive(true);
    }
    public void Back()
    {
        menubuttons.SetActive(true);
        backbutton.SetActive(false);
        credits.SetActive(false);
        optionsmenu.SetActive(false);
             
    }
    public void Options()
    {
        menubuttons.SetActive(false);
        optionsmenu.SetActive(true);
        backbutton.SetActive(true);
    }
    private YieldInstruction Instruction = new YieldInstruction();
    public IEnumerator FadeOut(Image image, float time)
    {//general fade out effect for images
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < time)
        {
            yield return Instruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / time);
            image.color = c;
        }
    }

    public IEnumerator FadeIn(Image image, float time)
    {//general fade in effect for images
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < time)
        {
            yield return Instruction;
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / time);
            image.color = c;
        }
    }
}
