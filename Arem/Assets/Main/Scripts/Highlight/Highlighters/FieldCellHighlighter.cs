using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FieldCellHighlighter : HighlighterBase
{
    [SerializeField] private Color _highlightColor;
    [SerializeField] private Vector3 _hightlightSize;

    private Color _defaultColor;
    private Vector3 _defaultSize;

    private SpriteRenderer _spriteRenderer;

    private Coroutine _highlightCoroutine;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultColor = _spriteRenderer.color;
        _defaultSize = transform.localScale;
    }


    public override void DisableHighlight()
    {
        if (_highlightCoroutine == null)
            return;

        StopCoroutine(_highlightCoroutine);
        _highlightCoroutine = null;

        _spriteRenderer.color = _defaultColor;
        transform.localScale = _defaultSize;
    }

    public override void EnableHighlight()
    {
        if (_highlightCoroutine != null)
            return;

        _highlightCoroutine = StartCoroutine(HighlightCoroutine());
    }


    private IEnumerator HighlightCoroutine()
    {
        _spriteRenderer.color = _highlightColor;

        while (true)
        {
            for (float lerp = 0f; lerp <= 1f; lerp += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(_defaultSize, _hightlightSize, lerp);
                yield return null;
            }

            for (float lerp = 0f; lerp <= 1f; lerp += Time.deltaTime)
            {
                transform.localScale = Vector3.Lerp(_hightlightSize, _defaultSize, lerp);
                yield return null;
            }
        }
    }
}