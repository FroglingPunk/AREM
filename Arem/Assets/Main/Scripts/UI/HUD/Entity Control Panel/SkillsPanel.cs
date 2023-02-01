using System.Collections.Generic;
using UnityEngine;

public class SkillsPanel : MonoBehaviour
{
    [SerializeField] private SkillButton _skillPrefab;

    private List<SkillButton> _activeButtons = new List<SkillButton>();
    private Queue<SkillButton> _buttonsPool = new Queue<SkillButton>();


    private void Start()
    {
        this.GetController<SkillsManager>().SelectedSkill.ValueChanged += OnSelectedSkillChanged;
    }


    public void Show(Skill[] skills)
    {
        Clear();

        for (int i = 0; i < skills.Length; i++)
        {
            var button = GetButtonFromPool();
            button.Init(skills[i]);
        }
    }

    public void Clear()
    {
        for (int i = _activeButtons.Count - 1; i >= 0; i--)
            ReturnButtonToPool(_activeButtons[i]);
    }


    private void OnSelectedSkillChanged(Skill skill)
    {
        _activeButtons.ForEach((button) =>
        {
            if (button.Skill == skill)
                button.Highlighter.EnableHighlight();
            else
                button.Highlighter.DisableHighlight();
        });
    }

    private SkillButton GetButtonFromPool()
    {
        var button = (SkillButton)default;

        if (_buttonsPool.Count > 0)
            button = _buttonsPool.Dequeue();
        else
            button = Instantiate(_skillPrefab, transform);

        _activeButtons.Add(button);
        button.gameObject.SetActive(true);
        return button;
    }

    private void ReturnButtonToPool(SkillButton button)
    {
        _activeButtons.Remove(button);
        _buttonsPool.Enqueue(button);

        button.Highlighter.DisableHighlight();
        button.gameObject.SetActive(false);
    }
}