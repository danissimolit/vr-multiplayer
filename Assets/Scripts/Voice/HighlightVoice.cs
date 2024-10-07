using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.PUN;

public class HighlightVoice : MonoBehaviour
{
    [SerializeField]
    private Image _micImage;

    [SerializeField]
    private Image _speakerImage;

    [SerializeField]
    private PhotonVoiceView _photonVoiceView;

    private void Awake()
    {
        _micImage.enabled = false;
        _speakerImage.enabled = false;
    }

    void Update()
    {
        _micImage.enabled = _photonVoiceView.IsRecording;
        _speakerImage.enabled = _photonVoiceView.IsSpeaking;
    }
}
