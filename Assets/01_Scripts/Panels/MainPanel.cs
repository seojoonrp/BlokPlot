using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    UserData currentUser = UserManager.Instance.CurrentUser;

    public TMP_Text welcomeText;
    public Button logoutButton;

    void Start()
    {
        if (currentUser != null)
        {
            welcomeText.text = UserManager.Instance.CurrentUser.username + ", Welcome!";
        }
        else
        {
            welcomeText.text = "No login information available.";
        }

        logoutButton.onClick.AddListener(OnLogoutButtonClick);
    }

    public void OnLogoutButtonClick()
    {
        UserManager.Instance.Logout();
        SceneManager.LoadScene("LoginScene");
    }
}
