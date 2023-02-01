using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AudioPlayer : MonoControllerBase
{
    private Queue<AudioSource> _freeAudioSources = new Queue<AudioSource>();
    private List<AudioSource> _allAudioSources = new List<AudioSource>();
    private AudioStorage _storage;


    private IEnumerator Start()
    {
        var handle =  Addressables.LoadAssetAsync<AudioStorage>("Audio Storage");

        yield return handle;

        _storage = handle.Result;
        _storage.Init();
    }

    public void PlayLoop(string name)
    {
        var clip = _storage.GetClip(name);

        var source = GetFreeAudioSource();

        source.loop = true;
        source.clip = clip;
        source.Play();
    }

    public void StopLoop(string name)
    {
        var clip = _storage.GetClip(name);
        var source = _allAudioSources.Find((s) => s.clip == clip);
        ReturnAudioSource(source);
    }

    public void PlayOneShot(string name)
    {
        StartCoroutine(PlayOneShotCoroutine(null));
    }

    public void StopAll()
    {
        _allAudioSources.ForEach(ReturnAudioSource);
    }


    private AudioSource GetFreeAudioSource()
    {
        if (_freeAudioSources.Count > 0)
            return _freeAudioSources.Dequeue();

        var newSourceGO = new GameObject($"Audio source #{_allAudioSources.Count}");
        newSourceGO.transform.SetParent(transform);

        var newSource = newSourceGO.AddComponent<AudioSource>();
        _allAudioSources.Add(newSource);

        return newSource;
    }

    private void ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.clip = null;
        _freeAudioSources.Enqueue(audioSource);
    }


    private IEnumerator PlayOneShotCoroutine(AudioClip clip)
    {
        var audioSource = GetFreeAudioSource();

        audioSource.PlayOneShot(clip);

        yield return new WaitForSeconds(clip.length);

        ReturnAudioSource(audioSource);
    }
}