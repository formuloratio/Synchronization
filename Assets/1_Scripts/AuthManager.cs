using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine.UI;
using System;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button logoutButton;

    private async void Awake()
    {
        UnityServices.Initialized += () => { Debug.Log("UGS 초기화 완료"); };

        // UGS 초기화
        await UnityServices.InitializeAsync(); //초기화 작업될 때까지 대기

        //버튼 이벤트 연결
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private async void OnLoginButtonClicked()
    {
        // 익명 로그인 요청
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
}
