using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputProvider inputProvider;

    public static InputManager Instance { get; private set; }

    public bool IsInputEnabled
    {
        get
        {
            return this.inputProvider.enabled;
        }
        set
        {
            this.inputProvider.enabled = value;
        }
    }

    private void Awake()
    {
        if (InputManager.Instance != null)
        {
            Destroy(base.gameObject);
        }

        InputManager.Instance = this;

        this.inputProvider = this.gameObject.AddComponent<InputProviderMouse>();

    }
}
