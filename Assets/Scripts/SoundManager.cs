using FMOD.Studio;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public ParentEventInstance ambientInstance;
    public ParentEventInstance musicInstance;
    private Bus MusicBus, SFXBus, SFXNoUIBus, MasterBus;
    private static SoundManager _SoundManagerInstance;
    public EventInstance pauseSnapshot;
    
    bool _isMusicPlaying = false;
    bool _isAmbientPlaying = false;
    private struct EventEmitterObject
    {
        public GameObject GameObject;
        public Transform Transform;
        public Dictionary<string,EventInstance> EventInstances;
    }
    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");

        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        SFXNoUIBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX/SFXNoUI");

        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");

        pauseSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Pause");
    }

    public void PlaySound2D(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path);
    }
    public class ParentEventInstance //for Ambience and Music
    {
        public EventInstance EventInstance {
            get;
        }
        public ParentEventInstance(string name,ParentEventInstance myEventInstance) {
            if(myEventInstance != null) {
                myEventInstance.Stop();
            }
            EventInstance = FMODUnity.RuntimeManager.CreateInstance(name);
            EventInstance.start();
        }
        public void Stop() {
            if(IsPlaying()) {
                EventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            }
        }
        public bool IsPlaying() {
            EventInstance.getPlaybackState(out PLAYBACK_STATE state);
            return state == PLAYBACK_STATE.PLAYING;
        }
        public void SetParameter(string parameter,float parametervalue) {
            EventInstance.setParameterByName(parameter,parametervalue);
        }
    }
    public void MusicBusSetVolume(float volume) {
        MusicBus.setVolume(volume);
    }
    public void SFXBusSetVolume(float volume) {
        SFXBus.setVolume(volume);
    }
    public void MasterBusSetVolume(float volume) {
        MasterBus.setVolume(volume);
    }
    public void Pause() {

        pauseSnapshot.start();
    }
    public void Resume() {

        pauseSnapshot.stop(STOP_MODE.ALLOWFADEOUT);
    }
    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene,LoadSceneMode mode) {
        if(!_isMusicPlaying) {
            musicInstance = new("event:/Music/MusicMain",musicInstance);
            _isMusicPlaying = true;
        }
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            musicInstance.SetParameter("Level",1f);
        }
        if(SceneManager.GetActiveScene().buildIndex != 0 && !_isAmbientPlaying) {
            ambientInstance = new("event:/SFX/AmbientMain",ambientInstance);
            _isAmbientPlaying = true;
        }
        else {
            if(ambientInstance != null) {
                ambientInstance.Stop();
                _isAmbientPlaying = false;
            }
        }
    }
}