using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake instance;
    
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    private float startingIntensity;
    private float shakeTimerTotal;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        instance = this; 
    }

    public void Shake(float intensity, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        shakeTimer = time;

        startingIntensity = intensity;
        shakeTimerTotal = time;
    }

    public void StartShake(float intensity, float frequency)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
    }

    public void EndShake()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f; 
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1f - shakeTimer / shakeTimerTotal);

        }


    }
}