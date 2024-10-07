using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using Photon.Realtime;

public class VoiceDebugUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _voiceState;

    private VoiceConnection _voiceConnection;
    
    private void Awake()
    {
        _voiceConnection = FindObjectOfType<VoiceConnection>();    
        if (_voiceConnection == null)
        {
            Debug.LogError("Voice Connection IS NULL.");
        }
    }

    private void OnEnable()
    {
        _voiceConnection.Client.StateChanged += VoiceClientStateChanged;
    }

    private void OnDisable()
    {
        _voiceConnection.Client.StateChanged -= VoiceClientStateChanged;
    }

    private void Update()
    {
        if (_voiceConnection ==null)
        {
            _voiceConnection = FindObjectOfType<VoiceConnection>();
        }       
    }


    private void VoiceClientStateChanged(Photon.Realtime.ClientState fromState, Photon.Realtime.ClientState toState)
    {
        UpdateUiBasedOnVoiceState(toState);
    }

    private void UpdateUiBasedOnVoiceState(Photon.Realtime.ClientState voiceClientState)
    {
        this._voiceState.text = string.Format("PhotonVoice: {0}", voiceClientState);
        if (voiceClientState == Photon.Realtime.ClientState.Joined)
        {
            _voiceState.gameObject.SetActive(false);
        }
    }
}

