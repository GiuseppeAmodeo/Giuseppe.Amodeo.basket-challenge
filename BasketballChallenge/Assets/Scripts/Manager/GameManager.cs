using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Match CurrentMatch { get; private set; }

    [SerializeField]
    private Match matchPrefab;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(base.gameObject);
        }
        GameManager.Instance = this;

        GameManager.CurrentMatch = Instantiate<Match>(matchPrefab); 
    }

}
