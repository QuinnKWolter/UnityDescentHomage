using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas MainScreen, ControlScreen, ExtraScreen;
    public Button Play, Controls, Extra, Quit, ReturnFromControls, ReturnFromExtras;

    // Start is called before the first frame update
    void Start()
    {
        Play.onClick.AddListener(PlayGame);
        Controls.onClick.AddListener(ShowControls);
        Extra.onClick.AddListener(ShowExtra);
        Quit.onClick.AddListener(QuitGame);
        ReturnFromControls.onClick.AddListener(ReturnToMainFromControls);
        ReturnFromExtras.onClick.AddListener(ReturnToMainFromExtras);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayGame(){
        SceneManager.LoadScene("Level01");
    }

    void QuitGame(){
        Application.Quit();
    }

    void ShowControls(){
        MainScreen.gameObject.SetActive(false);
        ExtraScreen.gameObject.SetActive(false);
        ControlScreen.gameObject.SetActive(true);
        ReturnFromControls.gameObject.SetActive(true);
    }

    void ShowExtra(){
        MainScreen.gameObject.SetActive(false);
        ControlScreen.gameObject.SetActive(false);
        ExtraScreen.gameObject.SetActive(true);
        ReturnFromExtras.gameObject.SetActive(true);
    }

    void ReturnToMainFromControls(){
        ControlScreen.gameObject.SetActive(false);
        ReturnFromControls.gameObject.SetActive(false);
        MainScreen.gameObject.SetActive(true);
    }

    void ReturnToMainFromExtras(){
        ExtraScreen.gameObject.SetActive(false);
        ReturnFromExtras.gameObject.SetActive(false);
        MainScreen.gameObject.SetActive(true);
    }

}
