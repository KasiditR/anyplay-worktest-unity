using System;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_Text _textDiamond;
    [SerializeField] private Slider _sliderHealthBar;
    [SerializeField] private Button _buttonAddDiamond;
    [SerializeField] private Button _buttonStart;

    public override void Show(Action onDone = null)
    {
        _textDiamond.text = CoreDataManager.Instance.UserData.diamonds.ToString();
        _sliderHealthBar.value = CoreDataManager.Instance.UserData.hearts;
        base.Show(onDone);
    }

    protected override void OnEnable()
    {
        _buttonAddDiamond.onClick.AddListener(OnButtonAddDiamondClicked);
        _buttonStart.onClick.AddListener(OnButtonStartClicked);
    }

    protected override void OnDisable()
    {
        _buttonAddDiamond.onClick.RemoveAllListeners();
        _buttonStart.onClick.RemoveAllListeners();
    }

    private void OnButtonStartClicked()
    {
        this.Hide();
        UICanvasManager.Instance.UIAuthentication.Show();
        CoreDataManager.Instance.ClearUserData();
    }

    private void OnButtonAddDiamondClicked()
    {
        JObject updateDiamondObject = new JObject();
        updateDiamondObject["id"] = CoreDataManager.Instance.UserData.id;
        APIManager.Instance.Put("updateDiamond", updateDiamondObject.ToString(), OnAddDiamondSuccess, OnAddDaimonFailure);
    }

    private void OnAddDiamondSuccess(string response)
    {
        JObject responseObject = JObject.Parse(response);
        _textDiamond.text = responseObject["diamonds"].ToString();
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

    private void OnAddDaimonFailure(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

}
