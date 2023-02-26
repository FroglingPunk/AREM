using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresEffectsController : MonoBehaviour
{
    [SerializeField] private SpriteFiguresGenerator _spriteFiguresGenerator;

    private Queue<SpriteRenderer> _pool = new Queue<SpriteRenderer>();


    private void Start()
    {
        this.GetController<MessageBus>().Subscribe<EntityHPChangedMessage>(OnEntityHPChanged);
    }


    private void OnEntityHPChanged(IMessage message)
    {
        var hpChangedMessage = message as EntityHPChangedMessage;

        var position = hpChangedMessage.Entity.transform.position + Vector3.up;
        var value = Mathf.Abs(hpChangedMessage.Delta);
        var color = (Color)default;

        if (value > 0)
            color = Color.green;
        else if (value < 0)
            color = Color.red;
        else
            color = Color.white;

        AddEffect(position, value, color);
    }


    private void AddEffect(Vector3 position, int value, Color color)
    {
        var element = GetElement();
        element.sprite = _spriteFiguresGenerator.Generate(value);
        element.color = color;
        element.transform.position = position;
        StartCoroutine(FigureEffectLifetimeCoroutine(element));
    }


    private SpriteRenderer GetElement()
    {
        if (_pool.Count == 0)
        {
            var element = new GameObject("Figures Effect").AddComponent<SpriteRenderer>();
            element.transform.SetParent(transform);
            return element;
        }
        else
        {
            var element = _pool.Dequeue();
            element.gameObject.SetActive(true);
            return element;
        }
    }

    private void ReturnToPool(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.gameObject.SetActive(false);
        _pool.Enqueue(spriteRenderer);
    }


    private IEnumerator FigureEffectLifetimeCoroutine(SpriteRenderer element)
    {
        var lifetime = 2f;

        var startColor = Color.red;
        var finishColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        var startPosition = element.transform.localPosition;
        var finishPosition = startPosition + Vector3.up;

        for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime / lifetime)
        {
            element.color = Color.Lerp(startColor, finishColor, lerp);
            element.transform.localPosition = Vector3.Lerp(startPosition, finishPosition, lerp);
            yield return null;
        }

        ReturnToPool(element);
    }
}