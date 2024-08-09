using UnityEngine;

public class UICanvasManager : Singleton<UICanvasManager>
{
    [SerializeField] private UIMessageBox uiMessageBox;
    [SerializeField] private UIAuthentication uiAuthentication;
    [SerializeField] private UILobby uiLobby;

    public UIMessageBox UIMessageBox { get => uiMessageBox; }
    public UIAuthentication UIAuthentication { get => uiAuthentication; }
    public UILobby UILobby { get => uiLobby; }

    public void Start()
    {
        uiAuthentication.Show();
        UIMessageBox.Hide();
        uiLobby.Hide();
    }
}
