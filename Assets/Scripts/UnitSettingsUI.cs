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

    private bool _isOpened;
    private bool _isSliding;

    private const float SlidingSpeed = .6f;
    private const float SlidingOffset = 850f;

    private void Start()
    {
        closeButton.onClick.AddListener(OnClose);
    }

    private void Awake()
    {
        FillSettingsForm(spawner.GetCurrentSettings());
    }

    private void SlideOpen()
    {
        if (_isSliding || _isOpened)
        {
            return;
        }
        
        _isOpened = true;
        StartCoroutine(Sliding());
    }
    
    private void SlideClose()
    {
        if (_isSliding || !_isOpened)
        {
            return;
        }
        
        _isOpened = false;
        StartCoroutine(Sliding());
    }
 
    private IEnumerator Sliding()
    {
        _isSliding = true;
        var startingPosition  = transform.position;
        var finalPosition = new Vector3(transform.position.x + SlidingOffset * (_isOpened ? -1 : 1),
            transform.position.y,
            transform.position.z);

        float elapsedTime = 0;
        while (elapsedTime < SlidingSpeed)
        {
            transform.position = Vector3.Lerp(startingPosition, finalPosition, (elapsedTime / SlidingSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isSliding = false;
    }

    private void OnClose()
    {
        var unitSettings = GetSettingsFromForm();
        spawner.ChangeUnitSettings(unitSettings);

        SlideClose();
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
