using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RecordingButton : MonoBehaviour
{
    private Button button;
    private Text buttonText;
    private bool isRecording = false;

    private Color initialColor = Color.green;
    private Color recordingColor = Color.red;

    void Start()
    {
        button = GetComponent<Button>();
        buttonText = button.GetComponentInChildren<Text>();
        UpdateButtonUI();
        button.onClick.AddListener(ToggleRecording);
    }

    public void ToggleRecording()
    {
        isRecording = !isRecording;
        UpdateButtonUI();
        if (isRecording)
        {
            // Start recording logic
            u6_slider_ctrl.Instance.StartRecording();
        }
        else
        {
            // Stop recording logic
            u6_slider_ctrl.Instance.StopRecording();
        }
    }

    void UpdateButtonUI()
    {
        buttonText.text = isRecording ? "Recording" : "Start Recording";
        buttonText.color = isRecording ? recordingColor : initialColor;
    }
}

