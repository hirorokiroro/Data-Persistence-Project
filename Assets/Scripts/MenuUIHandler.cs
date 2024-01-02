using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]


public class MenuUIHandler : MonoBehaviour
{

    //InputField���i�[���邽�߂̕ϐ�
    [SerializeField] InputField inputField;


    private void Update()
    {
        GlobalVariables.playerName = inputField.text;
        Debug.Log(GlobalVariables.playerName);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();  //original code to quit Unity Player
#endif
    }

}
