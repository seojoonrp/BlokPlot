using PimDeWitte.UnityMainThreadDispatcher;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private TMP_Text logText;
    [SerializeField] private TMP_InputField messageInput;
    [SerializeField] private Button connectButton;
    [SerializeField] private Button sendButton;

    private WebSocket ws;

    void Start()
    {
        logText.text = "";
        connectButton.onClick.AddListener(ConnectToServer);
        sendButton.onClick.AddListener(OnSendButtonClick);
    }

    private void ConnectToServer()
    {
        if (ws != null && ws.IsAlive)
        {
            Debug.Log("Already connected.");
            return;
        }

        ws = new("ws://localhost:8080/ws");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connection open!");
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                logText.text += "\nServer Connected!";
                connectButton.interactable = false;
                sendButton.interactable = true;
            });
        };

        ws.OnMessage += (sender, e) =>
        {
            string message = e.Data;
            Debug.Log("Message received from Server: " + message);
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                logText.text += "\nReceived: " + message;
            });
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("Error: " + e.Message);
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                logText.text += "\n<color=red>Error: " + e.Message;
            });
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("Connection closed.");
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                logText.text += "\nConnection Closed.";
                connectButton.interactable = true;
                sendButton.interactable = false;
            });
        };

        ws.Connect();
    }

    private void OnSendButtonClick()
    {
        if (ws == null || !ws.IsAlive)
        {
            logText.text += "\nNot connected to server.";
            return;
        }

        string message = messageInput.text;
        if (string.IsNullOrEmpty(message)) return;

        ws.Send(message);

        logText.text += "\nSent: " + message;
        messageInput.text = "";
    }

    private void OnDestroy()
    {
        if (ws != null)
        {
            ws.Close();
            ws = null;
        }
    }
}
