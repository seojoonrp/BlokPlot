using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LandingPageManager : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public Button loginButton;
    public GameObject loadingIndicator;
    public TMP_Text errorText;

    private AuthService authService;

    void Start()
    {
        authService = new();

        loginButton.onClick.AddListener(OnLoginButtonClick);
        loadingIndicator.SetActive(false);
        errorText.text = "";
    }

    public void OnLoginButtonClick()
    {
        string username = usernameInput.text;
        if (string.IsNullOrEmpty(username))
        {
            errorText.text = "닉네임을 입력해주세요.";
            return;
        }

        loginButton.interactable = false;
        loadingIndicator.SetActive(true);
        errorText.text = "";

        StartCoroutine(authService.LoginOrRegister(username, OnLoginSuccess, OnLoginFail));
    }

    private void OnLoginSuccess(UserData user)
    {
        UserManager.Instance.SetCurrentUser(user);
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnLoginFail(string errorMessage)
    {
        loginButton.interactable = true;
        loadingIndicator.SetActive(false);
        errorText.text = errorMessage;
    }
}
