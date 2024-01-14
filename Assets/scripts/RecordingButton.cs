using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordingButton : MonoBehaviour
{
    [SerializeField] private Button button; 
    private Text buttonText;
    private bool isRecording = false;

    private Color initialColor = Color.green;
    private Color recordingColor = Color.red;

    void Start()
    {
        buttonText = button.GetComponentInChildren<Text>();
        button.onClick.AddListener(ToggleRecording);
    }

    public void ToggleRecording()
    {
        isRecording = !isRecording;
        UpdateButtonUI();
        // Update button UI
        buttonText.text = isRecording ? "Recording" : "Start Recording";
        buttonText.color = isRecording ? recordingColor : initialColor;
        if (isRecording)
        {
            u6_slider_ctrl.Instance.StartRecording();
        }
        else
        {
            u6_slider_ctrl.Instance.StopRecording();
        }
    }

    void UpdateButtonUI()
    {
        buttonText.text = isRecording ? "Recording" : "Start Recording";
        buttonText.color = isRecording ? recordingColor : initialColor;
    }
}



