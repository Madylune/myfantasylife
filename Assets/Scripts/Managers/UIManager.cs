﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private GameObject targetFrame;

    public Text targetName;
    public Text targetLevel;
    public Image targetIcon;
    public HealthBar targetHealthBar;

    private Stats stats;

    private GameObject[] keybindsButtons;

    private void Awake() 
    {
        keybindsButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }

    private void Start() 
    {   
        stats = targetFrame.GetComponentInChildren<Stats>();
        if (stats != null)
        {
            targetHealthBar.SetMaxHealth(stats.maxHealth);
        }

        SetUseable(actionButtons[0], SpellBook.MyInstance.GetSpell("Fireball"));
        SetUseable(actionButtons[1], SpellBook.MyInstance.GetSpell("Lightningbolt"));
        SetUseable(actionButtons[2], SpellBook.MyInstance.GetSpell("Waterball"));
    }

    private void Update() 
    {   
        if (stats != null)
        {
            targetHealthBar.SetHealth(stats.currentHealth);
        }
    }


    public void ShowTargetFrame(NPC target)
    {
        UpdateTargetFrame(target);

        targetFrame.SetActive(true);
    }

    public void HideTargetFrame()
    {
        targetFrame.SetActive(false);
        stats = null;
    }

    public void UpdateTargetFrame(NPC target)
    {
        stats = target.GetComponent<Stats>();

        targetName.text = stats.name;
        targetLevel.text = stats.level.ToString();
        targetIcon.sprite = stats.icon;
        
        targetHealthBar.SetHealth(stats.currentHealth);
    }

    public void TogglePanel(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true; 
    }

    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindsButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyIcon.sprite = useable.MyIcon;
        btn.MyIcon.color = Color.white;

        btn.MyUseable = useable;
    }
}
