using System;
using System.Collections;
using Configuration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSettingsUI : MonoBehaviour
{
    [SerializeField] private UnitSpawner spawner;
    
    [SerializeField] private TMP_InputField maxHealthInput;
    [SerializeField] private TMP_InputField speedInput;
    [SerializeField] private TMP_InputField missChanceInput;
    [SerializeField] private TMP_InputField critChanceInput;
    [SerializeField] private TMP_InputField quickAttackDamageInput;
    [SerializeField] private TMP_InputField quickAttackChanceInput;
    [SerializeField] private TMP_InputField strongAttackDamageInput;
    [SerializeField] private TMP_InputField strongAttackChanceInput;

    [SerializeField] private Button closeButton;

    private float _menuOffset;
    private bool _isOpened;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClose);
        
        _menuOffset = transform.localScale.x / 2;
        
        FillSettingsForm(spawner.GetCurrentSettings());
    }
    
    private void Open()
    {
        _isOpened = true;
        StartCoroutine(SlideClose());
    }
 
    private IEnumerator SlideClose()
    {
        while (_menuOffset > .3f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + _menuOffset,
                transform.position.y,
                transform.position.z), Time.deltaTime * 10f);

            _menuOffset -= Time.deltaTime;
            
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void OnClose()
    {
        var unitSettings = GetSettingsFromForm();
        spawner.ChangeUnitSettings(unitSettings);

        _isOpened = false;
    }

    private UnitSettings GetSettingsFromForm()
    {
        var unitSettings = spawner.GetCurrentSettings();

        maxHealthInput.TryGetValueTo(ref unitSettings.maxHealth);
        speedInput.TryGetValueTo(ref unitSettings.speed);
        critChanceInput.TryGetValueTo(ref unitSettings.critChance);
        missChanceInput.TryGetValueTo(ref unitSettings.missChance);
        quickAttackDamageInput.TryGetValueTo(ref unitSettings.quickAttack.damage);
        quickAttackChanceInput.TryGetValueTo(ref unitSettings.quickAttack.chanceToUse);
        strongAttackDamageInput.TryGetValueTo(ref unitSettings.strongAttack.damage);
        strongAttackChanceInput.TryGetValueTo(ref unitSettings.strongAttack.chanceToUse);

        return unitSettings;
    }

    private void FillSettingsForm(UnitSettings settings)
    {
        maxHealthInput.text = $"{settings.maxHealth}";
        speedInput.text = $"{settings.speed}";
        critChanceInput.text = $"{settings.critChance}";
        missChanceInput.text = $"{settings.missChance}";
        quickAttackDamageInput.text = $"{settings.quickAttack.damage}";
        quickAttackChanceInput.text = $"{settings.quickAttack.chanceToUse}";
        strongAttackDamageInput.text = $"{settings.strongAttack.damage}";
        strongAttackChanceInput.text = $"{settings.strongAttack.chanceToUse}";
    }
}
