using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public List<GameObject> panels;

    private GameObject currentPanel = null;

    // 맨 처음 열려있는 패널을 찾아서 초기 패널로 설정
    void Start()
    {
        foreach (var panel in panels)
        {
            if (panel.activeSelf)
            {
                currentPanel = panel;
                break;
            }
        }
    }

    public void OpenPanel(GameObject panel)
    {
        if (currentPanel != null) currentPanel.SetActive(false);

        panel.SetActive(true);
        currentPanel = panel;
    }
}
