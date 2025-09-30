using TMPro;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    UserData currentUser = UserManager.Instance.CurrentUser;

    public TMP_Text coinText;

    void Start()
    {
        if (currentUser != null)
        {
            coinText.text = "Coins: " + currentUser.stats.coins;
        }
        else
        {
            coinText.text = "로그인하거라";
        }
    }
}
