using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkillButtonUI : MonoBehaviour
{
    public static SkillButtonUI Instance;
    [Header("Skills")]
    [Header("Undo")]
    [SerializeField] private Button undoSkillButton;
    [SerializeField] private Button undoSkillADSButton;
    [SerializeField] private TextMeshProUGUI undoSkillAmount;
    [SerializeField] private TextMeshProUGUI adUndoSKill;

    [Header("Hint")]
    [SerializeField] private Button hintSkillButton;
    [SerializeField] private Button hintSkillADSButton;
    [SerializeField] private TextMeshProUGUI hintSkillAmount;
    [SerializeField] private TextMeshProUGUI adHintSKill;

    [Header("Shuffle")]
    [SerializeField] private Button shuffleSkillButton;
    [SerializeField] private Button shuffleSkillADSButton;
    [SerializeField] private TextMeshProUGUI shuffleSkillAmount;
    [SerializeField] private TextMeshProUGUI adShuffleSKill;

    public int _undoSKillAmount;
    public int _hintSkillAmount;
    public int _shuffleSkillAmount;

    private void Awake()
    {
        if (Instance != null) Destroy(Instance);
        else Instance = this;
        LoadAmountSkill();
        UpdateSkill();
    }
    public void Undo()
    {
        SkillManager.instance.UndoTile();
        UpdateSkill();
    }
    public void Hint()
    {
        SkillManager.instance.HintTile();
        UpdateSkill();
    }
    public void Shuffle()
    {
        SkillManager.instance.ShuffleTile();
        _shuffleSkillAmount--;
        CameraShake.Instance.Shake(0.15f, 0.05f);
        UpdateSkill();
    }
    public void ADUndo()
    {
        // AdsManager.instance.ShowRewardedAd();
        _undoSKillAmount++;
        UpdateSkill();
    }
    public void ADHint()
    {
        // AdsManager.instance.ShowRewardedAd();
        _hintSkillAmount++;
        UpdateSkill();
    }
    public void ADShuffle()
    {
        // AdsManager.instance.ShowRewardedAd();
        _shuffleSkillAmount++;
        UpdateSkill();
    }

    private void UpdateSkill()
    {
        // Update skill amounts in the UI
        undoSkillAmount.text = _undoSKillAmount.ToString();
        hintSkillAmount.text = _hintSkillAmount.ToString();
        shuffleSkillAmount.text = _shuffleSkillAmount.ToString();

        // Update Undo Skill UI
        if (_undoSKillAmount <= 0)
        {
            undoSkillAmount.gameObject.SetActive(false);
            adUndoSKill.gameObject.SetActive(true);
            undoSkillButton.interactable = false;
            undoSkillADSButton.enabled = true;
        }
        else
        {
            undoSkillAmount.gameObject.SetActive(true);
            adUndoSKill.gameObject.SetActive(false);
            undoSkillButton.interactable = true;
            undoSkillADSButton.enabled = false;
        }

        // Update Hint Skill UI
        if (_hintSkillAmount <= 0)
        {
            hintSkillAmount.gameObject.SetActive(false);
            adHintSKill.gameObject.SetActive(true);
            hintSkillButton.interactable = false;
            hintSkillADSButton.enabled = true;
        }
        else
        {
            hintSkillAmount.gameObject.SetActive(true);
            adHintSKill.gameObject.SetActive(false);
            hintSkillButton.interactable = true;
            hintSkillADSButton.enabled = false;
        }

        // Update Shuffle Skill UI
        if (_shuffleSkillAmount <= 0)
        {
            shuffleSkillAmount.gameObject.SetActive(false);
            adShuffleSKill.gameObject.SetActive(true);
            shuffleSkillButton.interactable = false;
            shuffleSkillADSButton.enabled = true;
        }
        else
        {
            shuffleSkillAmount.gameObject.SetActive(true);
            adShuffleSKill.gameObject.SetActive(false);
            shuffleSkillButton.interactable = true;
            shuffleSkillADSButton.enabled = false;
        }

        // Update Ad Buttons Interactivity
        // bool isAdReady = AdsManager.instance != null && AdsManager.instance.isRewardedAdReady;

        // if (undoSkillADSButton.enabled)
        // {
        //     undoSkillADSButton.interactable = isAdReady;
        // }
        // if (hintSkillADSButton.enabled)
        // {
        //     hintSkillADSButton.interactable = isAdReady;
        // }
        // if (shuffleSkillADSButton.enabled)
        // {
        //     shuffleSkillADSButton.interactable = isAdReady;
        // }

        // Save the updated skill amounts
        SaveAmountSkill();
    }
    private void LoadAmountSkill()
    {
        _undoSKillAmount = PlayerPrefs.GetInt("Amount Undo Skill", 10);
        _hintSkillAmount = PlayerPrefs.GetInt("Amount Hint Skill", 10);
        _shuffleSkillAmount = PlayerPrefs.GetInt("Amount Shuffle Skill", 10);
    }
    private void SaveAmountSkill()
    {
        PlayerPrefs.SetInt("Amount Undo Skill", _undoSKillAmount);
        PlayerPrefs.SetInt("Amount Hint Skill", _hintSkillAmount);
        PlayerPrefs.SetInt("Amount Shuffle Skill", _shuffleSkillAmount);
    }
}
