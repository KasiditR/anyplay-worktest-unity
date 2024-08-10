using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;

public class UILobby : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_Text _textDiamond;
    [SerializeField] private Slider _sliderHealthBar;
    [SerializeField] private Button _buttonAddDiamond;
    [SerializeField] private Button _buttonStart;

    private Coroutine _putCoroutine;
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

        if (_putCoroutine != null)
        {
            StopCoroutine(_putCoroutine);
        }
        _putCoroutine = StartCoroutine(APIManager.Instance.PutRoutine("updateDiamond", updateDiamondObject.ToString(), OnAddDiamondSuccess, OnAddDaimonFailure));
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
