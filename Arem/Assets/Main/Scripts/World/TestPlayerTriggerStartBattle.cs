using UnityEngine;

public class TestPlayerTriggerStartBattle : PlayerTriggerBase
{
    [SerializeField] private GameObject _battleContent;


    protected override void OnPlayerEnter(TestPlayer player)
    {
        player.gameObject.SetActive(false);
        Instantiate(_battleContent);
        Destroy(gameObject);
    }

    protected override void OnPlayerExit(TestPlayer player)
    {

    }
}