using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Storage", menuName = "Audio/Storage", order = 0)]
public class AudioStorage : ScriptableObject
{
    [SerializeField] private AudioAsset[] _audioAssets;

    private Dictionary<string, AudioClip> _clips;


    public void Init()
    {
        _clips = new Dictionary<string, AudioClip>();

        for (int i = 0; i < _audioAssets.Length; i++)
            _clips.Add(_audioAssets[i].Name, _audioAssets[i].Clip);
    }


    public AudioClip GetClip(string name)
    {
        if (!_clips.ContainsKey(name))
        {
            Debug.LogError($"Not found Audio Clip for {name} Key");
            return null;
        }

        return _clips[name];
    }
}