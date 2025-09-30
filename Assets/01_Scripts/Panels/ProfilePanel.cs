using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
    UserData currentUser = UserManager.Instance.CurrentUser;

    public TMP_Text winsText;
    public TMP_Text liarWinsText;
    public TMP_Text avatarUrl;
    public TMP_Text bannerUrl;

    void Start()
    {
        if (currentUser != null)
        {
            winsText.text = "Wins: " + currentUser.stats.wins;
            liarWinsText.text = "Liar Wins: " + currentUser.stats.liarWins;
            avatarUrl.text = "Avatar: " + currentUser.profile.avatarUrl;
            bannerUrl.text = "Banner: " + currentUser.profile.bannerUrl;
        }
        else
        {
            winsText.text = "Wins: 0";
            liarWinsText.text = "Wins: 0";
            avatarUrl.text = "Avatar: None";
            bannerUrl.text = "Banner: None";
        }
    }
}
