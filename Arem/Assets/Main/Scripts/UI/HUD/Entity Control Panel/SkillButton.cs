using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IHighlightable
{
    [SerializeField] private Image _imageContent;

    public HighlighterBase Highlighter => GetComponentInChildren<SkillButtonHighlighter>();
    public Skill Skill { get; private set; }


    public void Init(Skill skill)
    {
        Skill = skill;
        _imageContent.sprite = skill.Sprite;
    }
}