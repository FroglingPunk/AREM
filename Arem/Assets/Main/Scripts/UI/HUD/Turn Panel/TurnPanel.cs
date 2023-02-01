using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    [SerializeField] private Text _textRoundNumber;
    [SerializeField] private Text _textArrow;


    private void Start()
    {
        this.GetController<TurnController>().CurrentTurnEntity.ValueChanged += OnCurrentTurnEntityChanged;
        this.GetController<TurnController>().Turn.ValueChanged += OnTurnChanged;
    }


    private void OnCurrentTurnEntityChanged(Entity entity)
    {
        _textArrow.text = entity.Team == ETeam.Player ? "<====" : "====>";
    }

    private void OnTurnChanged(int round)
    {
        _textRoundNumber.text = $"Раунд {round}";
    }
}