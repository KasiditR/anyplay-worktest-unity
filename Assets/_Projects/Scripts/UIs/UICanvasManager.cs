using UnityEngine;

public class UICanvasManager : Singleton<UICanvasManager>
{
    [SerializeField] private UIMessageBox _uiMessageBox;
    [SerializeField] private UIAuthentication _uiAuthentication;
    [SerializeField] private UILobby _uiLobby;

    public UIMessageBox UIMessageBox { get => _uiMessageBox; }
    public UIAuthentication UIAuthentication { get => _uiAuthentication; }
    public UILobby UILobby { get => _uiLobby; }

    public void Start()
    {
        _uiAuthentication.Show();
        UIMessageBox.Hide();
        _uiLobby.Hide();
    }
}
