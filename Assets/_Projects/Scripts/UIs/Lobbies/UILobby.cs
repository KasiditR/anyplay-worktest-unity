using System;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_Text textDiamond;
    [SerializeField] private Slider sliderHealthBar;
    [SerializeField] private Button buttonAddDiamond;
    [SerializeField] private Button buttonStart;

    public override void Show(Action onDone = null)
    {
        textDiamond.text = CoreDataManager.Instance.UserData.diamonds.ToString();
        sliderHealthBar.value = CoreDataManager.Instance.UserData.hearts;
        base.Show(onDone);
    }

    protected override void OnEnable()
    {
        buttonAddDiamond.onClick.AddListener(OnButtonAddDiamondClicked);
        buttonStart.onClick.AddListener(OnButtonStartClicked);
    }

    protected override void OnDisable()
    {
        buttonAddDiamond.onClick.RemoveAllListeners();
        buttonStart.onClick.RemoveAllListeners();
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
        textDiamond.text = responseObject["diamonds"].ToString();
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

    private void OnAddDaimonFailure(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

}
