using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button signUpButton;

    [SerializeField] private TMP_InputField useNameIF_SignUp;
    [SerializeField] private TMP_InputField passwordIF_SignUp;
    [SerializeField] private TMP_InputField useNameIF_Login;
    [SerializeField] private TMP_InputField passwordIF_Login;

    public static event Action OnSignUpSuccess;
    public static event Action OnLoginSuccess;

    private async void Awake()
    {
        UnityServices.Initialized += () =>
        {
            Debug.Log("UGS 초기화 완료");
        };

        // UGS 초기화
        await UnityServices.InitializeAsync(); //초기화 작업될 때까지 대기

        // 인증관련 이벤트 연결
        EventBinding();

        // 회원가입
        signUpButton.onClick.AddListener( async () =>
        {
            var userName = useNameIF_SignUp.text;
            var password = passwordIF_SignUp.text;
            try
            {
                await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(userName, password);
                OnSignUpSuccess?.Invoke();
                AuthenticationService.Instance.SignOut(); //회원가입 후 자동로그인 방지 위해 로그아웃 처리
                Debug.Log("회원가입 성공");
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("회원가입 실패: " + e.Message);
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("회원가입 실패: " + e.Message);
            }
        });

        //로그인
        loginButton.onClick.AddListener( async () =>
        {
            var userName = useNameIF_Login.text;
            var password = passwordIF_Login.text;
            try
            {
                await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(userName, password);
                OnLoginSuccess?.Invoke();
                Debug.Log("로그인 성공");
            }
            catch (AuthenticationException e)
            {
                Debug.LogError("로그인 실패: " + e.Message);
            }
            catch (RequestFailedException e)
            {
                Debug.LogError("로그인 실패: " + e.Message);
            }
        });

        //로그아웃
        logoutButton.onClick.AddListener( () =>
        {
            AuthenticationService.Instance.SignOut();
        });
    }

    private void EventBinding()
    {
        // 로그인 성공
        AuthenticationService.Instance.SignedIn += async () =>
        {
            Debug.Log("로그인 성공! 플레이어 ID: " + AuthenticationService.Instance.PlayerId);

            // 플레이어 이름 가져오기
            await AuthenticationService.Instance.GetPlayerNameAsync();
            var playerName = AuthenticationService.Instance.PlayerName;
        };

        // 로그아웃
        AuthenticationService.Instance.SignedOut += () =>
        {
            Debug.Log("로그아웃 되었습니다.");
        };

        //로그인 실패
        AuthenticationService.Instance.SignInFailed += (err) =>
        {
            Debug.Log("로그인 실패: " + err);
        };

        //세션 아웃
        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("세션이 만료되었습니다.");
        };
    }

}
