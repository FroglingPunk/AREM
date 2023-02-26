using UnityEngine;

public class SpriteFiguresGenerator : MonoBehaviour
{
    [SerializeField] private Sprite _spriteNumbers;

    private int _figureWidth;
    private int _figureHeight;


    private void Awake()
    {
        _figureWidth = _spriteNumbers.texture.width / 10;
        _figureHeight = _spriteNumbers.texture.height;
    }


    public Sprite Generate(int number)
    {
        if (number < 0)
            return default;

        var figuresCount = number == 0 ? 1 : (int)Mathf.Log10(number) + 1;
        var rectSize = new Rect(0f, 0f, _figureWidth * figuresCount, _figureHeight);
        var genTexture = new Texture2D((int)rectSize.width, (int)rectSize.height);
        var result = 0;

        while (number > 0)
        {
            result *= 10;
            result += number % 10;
            number /= 10;
        }

        for (var i = 0; i < figuresCount; i++)
        {
            AddFigureToTexture2D(ref genTexture, result % 10, i);
            result /= 10;
        }

        genTexture.Apply();

        return Sprite.Create(genTexture, rectSize, Vector2.zero);
    }


    private void AddFigureToTexture2D(ref Texture2D texture, int figure, int positionDelta)
    {
        if (figure > 9 || figure < 0)
            return;

        var figuresSpriteDelta = figure * _figureWidth;
        var genTextureDelta = positionDelta * _figureWidth;

        var pixels = _spriteNumbers.texture.GetPixels(figuresSpriteDelta, 0, _figureWidth, _figureHeight);
        texture.SetPixels(genTextureDelta, 0, _figureWidth, _figureHeight, pixels);
    }
}