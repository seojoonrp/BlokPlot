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
            welcomeText.text = UserManager.Instance.CurrentUser.username + ", Welcome!";
        }
        else
        {
            welcomeText.text = "No login information available.";
        }
    }
}
