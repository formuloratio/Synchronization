using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SignupUI;
    [SerializeField] private GameObject LoginUI;

    [SerializeField] private Button SignUpClosedButton;

    private void Awake()
    {
        SignUpClosedButton.onClick.AddListener(CloseSignUpUI);
    }

    private void OnEnable()
    {
        AuthManager.OnSignUpSuccess += OnClickSignUpButton;
        AuthManager.OnLoginSuccess += OnClickLoginButton;
    }

    private void OnDisable()
    {
        AuthManager.OnSignUpSuccess -= OnClickSignUpButton;
        AuthManager.OnLoginSuccess -= OnClickLoginButton;
    }

    private void OnClickSignUpButton()
    {
        SignupUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    private void OnClickLoginButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void CloseSignUpUI()
    {
        SignupUI.SetActive(false);
        LoginUI.SetActive(true);
    }
}
