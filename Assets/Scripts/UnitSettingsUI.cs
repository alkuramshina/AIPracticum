using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Configuration;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSettingsUI : MonoBehaviour
{
    [SerializeField] private UnitSpawner spawner;
    [SerializeField] private Transform parentPanel;
    
    [SerializeField] private Transform inputGroupPrefab;
    [SerializeField] private TextMeshProUGUI inputLabelPrefab;
    [SerializeField] private TMP_InputField inputPrefab;

    private UnitSettings _dirtySettings;
    private bool _isOpened;

    private void Awake()
    {
        _dirtySettings = spawner.GetCurrentSettings();
        
        FillSettingsForm();
        
        _isOpened = false;
        gameObject.SetActive(false);
    }
    
    private void Open()
    {
        _isOpened = true;
        gameObject.SetActive(true);
    }

    private void Close()
    {
        spawner.ChangeUnitSettings(_dirtySettings);
        
        gameObject.SetActive(false);
        _isOpened = false;
    }
    
    private void FillSettingsForm()
    {
        var fieldsToAdd = typeof(UnitSettings).GetFieldsToShowInSettings(_dirtySettings);
        
        foreach (var (fieldTitle, fieldValue) in fieldsToAdd)
        {
            var inputGroup = Instantiate(inputGroupPrefab, parentPanel);
            
            var text = Instantiate(inputLabelPrefab, inputGroup);
            text.text = fieldTitle;

            var input = Instantiate(inputPrefab, inputGroup);
            input.text = fieldValue;
        }
    }
}
