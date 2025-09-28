using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField usernameInput;
    public Button loginButton;
    public GameObject loadingIndicator;
    public TMP_Text errorText;

    [System.Serializable]
    private class LoginRequestBody { public string username; }

    void Start()
    {
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
        StartCoroutine(LoginOrRegisterCoroutine(username));
    }

    IEnumerator LoginOrRegisterCoroutine(string username)
    {
        loginButton.interactable = false;
        loadingIndicator.SetActive(true);
        errorText.text = "";

        string url = "http://localhost:8080/api/login";

        LoginRequestBody requestBody = new() { username = username };
        string jsonBody = JsonUtility.ToJson(requestBody);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        using (UnityWebRequest webRequest = new(url, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                errorText.text = "로그인에 실패했습니다. 서버 상태를 확인해주세요.";
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                UserData receivedUser = JsonUtility.FromJson<UserData>(webRequest.downloadHandler.text);
                UserManager.Instance.SetCurrentUser(receivedUser);

                SceneManager.LoadScene("MainMenuScene");
            }
        }
    }
}
