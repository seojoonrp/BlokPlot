using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UserData
{
    public string username;
    public ProfileData profile;
    public StatsData stats;
}
[System.Serializable]
public class ProfileData { public string avatarUrl, bannerUrl; }
[System.Serializable]
public class StatsData { public int wins, liarWins, trophies, coins; }

public class UserManager : MonoBehaviour
{
    public static UserManager Instance;
    public UserData CurrentUser;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentUser(UserData user)
    {
        CurrentUser = user;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
