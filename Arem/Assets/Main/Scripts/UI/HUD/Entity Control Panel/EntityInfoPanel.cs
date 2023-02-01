using UnityEngine;
using UnityEngine.UI;

public class EntityInfoPanel : MonoBehaviour
{
    [SerializeField] private Text _textDamage;
    [SerializeField] private Text _textArmor;
    [SerializeField] private Text _textCriticalDamageChance;
    [SerializeField] private Text _textDodgeChance;
    [SerializeField] private Text _textSpeed;


    public void Show(Entity entity)
    {
        var stats = entity.Stats;

        _textDamage.text = stats.Damage.ToString();
        _textArmor.text = stats.Armor.ToString();
        _textCriticalDamageChance.text = stats.CriticalDamageChance.ToString();
        _textDodgeChance.text = stats.DodgeChance.ToString();
        _textSpeed.text = stats.Speed.ToString();
    }

    public void Hide()
    {
        _textDamage.gameObject.SetActive(false);
        _textArmor.gameObject.SetActive(false);
        _textCriticalDamageChance.gameObject.SetActive(false);
        _textDodgeChance.gameObject.SetActive(false);
        _textSpeed.gameObject.SetActive(false);
    }

    public void Clear()
    {
        _textDamage.text = string.Empty;
        _textArmor.text = string.Empty;
        _textCriticalDamageChance.text = string.Empty;
        _textDodgeChance.text = string.Empty;
        _textSpeed.text = string.Empty;
    }
}