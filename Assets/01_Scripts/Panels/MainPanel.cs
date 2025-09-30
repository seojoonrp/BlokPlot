using TMPro;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    UserData currentUser = UserManager.Instance.CurrentUser;

    public TMP_Text welcomeText;

    void Start()
    {
        if (currentUser != null)
        {
            welcomeText.text = UserManager.Instance.CurrentUser.username + "님, 환영합니다!";
        }
        else
        {
            welcomeText.text = "로그인 정보가 없습니다.";
        }
    }
}
