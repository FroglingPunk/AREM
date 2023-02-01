using UnityEngine;

public class EntityControlPanel : MonoBehaviour
{
    private EntityInfoPanel _entityInfoPanel;
    private SkillsPanel _skillsPanel;


    private void Start()
    {
        _entityInfoPanel = GetComponentInChildren<EntityInfoPanel>();
        _skillsPanel = GetComponentInChildren<SkillsPanel>();

        this.GetController<TurnController>().CurrentTurnEntity.ValueChanged += OnCurrentTurnEntityChanged;
    }


    private void OnCurrentTurnEntityChanged(Entity entity)
    {
        _entityInfoPanel.Show(entity);
        _skillsPanel.Show(entity.Skills);
    }
}