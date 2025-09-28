using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public TMP_Text welcomeText;

    void Start()
    {
        if (UserManager.Instance != null && UserManager.Instance.CurrentUser != null)
        {
            welcomeText.text = UserManager.Instance.CurrentUser.username + "님, 환영합니다!";
        }
        else
        {
            welcomeText.text = "로그인 정보가 없습니다.";
        }
    }
}
