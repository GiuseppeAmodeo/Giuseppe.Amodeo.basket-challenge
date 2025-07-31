using System;
using UnityEngine;

public static class PlayerPreferences 
{
    private const string audioKey = "audio";

    private static bool isInitialized;
    public static bool IsAudioEnabled
    {
        get
        {
            PlayerPreferences.Init();
            return PlayerPrefs.GetString("audio") == true.ToString();
        }
        set
        {
            PlayerPreferences.Init();
            PlayerPrefs.SetString("audio", value.ToString());
            PlayerPreferences.Save();
        }
    }
    private static void Init()
    {
        if (!PlayerPreferences.isInitialized)
        {
            PlayerPreferences.isInitialized = true;
            if (!PlayerPrefs.HasKey("audio"))
            {
                PlayerPreferences.IsAudioEnabled = true;
            }
        }
    }
    private static void Save()
    {
        PlayerPrefs.Save();
    }
}
