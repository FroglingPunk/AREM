using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SkillButtonHighlighter : HighlighterBase
{
    [SerializeField] private Color _highlightColor;

    private Color _defaultColor;
    private Image _image;


    private void Start()
    {
        _image = GetComponent<Image>();
        _defaultColor = _image.color;
    }


    public override void DisableHighlight()
    {
        _image.color = _defaultColor;
    }

    public override void EnableHighlight()
    {
        _image.color = _highlightColor;
    }
}