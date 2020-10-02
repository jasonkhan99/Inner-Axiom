﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenuPanelController : MonoBehaviour
{
    const string ShowKey = "Show";
    const string HideKey = "Hide";
    const string EntryPoolKey = "AbilityMenuPanel.Entry";
    const int MenuCount = 4;
    [SerializeField] GameObject entryPrefab;
    [SerializeField] Text titleLabel;
    [SerializeField] Panel panel;
    [SerializeField] GameObject canvas;
    List<AbilityMenuEntry> menuEntries = new List<AbilityMenuEntry>(MenuCount);
    public int selection { get; private set; }

    void Awake ()
    {
        GameObjectPoolController.AddEntry(EntryPoolKey, entryPrefab, MenuCount, int.MaxValue);
    }

    AbilityMenuEntry Dequeue ()
    {
        Poolable p = GameObjectPoolController.Dequeue(EntryPoolKey);
        AbilityMenuEntry entry = p.GetComponent<AbilityMenuEntry>();
        entry.transform.SetParent(panel.transform, false);
        entry.transform.localScale = Vector3.one;
        entry.gameObject.SetActive(true);
        entry.Reset();
        return entry;
    }
    void Enqueue (AbilityMenuEntry entry)
    {
        Poolable p = entry.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }

    void Clear ()
    {
        for (int i = menuEntries.Count - 1; i >= 0; --i)
        {
            Enqueue(menuEntries[i]);
            menuEntries.Clear();
        }
    }

    Tweener TogglePos (string pos)
    {
        Tweener t = panel.SetPosition(pos, true);
        t.easingControl.duration = 0.5f;
        t.easingControl.equation = EasingEquations.EaseOutQuad;
        return t;
    }

    bool SetSelection (int value)
    {
        if (menuEntries[value].IsLocked)
        {
            return false;
        }
        // Deselect the previously selected entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = false;
            selection = value;
        }
        // Select the new entry
        if (selection >= 0 && selection < menuEntries.Count)
        {
            menuEntries[selection].IsSelected = true;
            return true;
        }
    }

    void Start ()
    {
        panel.SetPosition(HideKey, false);
        canvas.SetActive(false);
    }
}
