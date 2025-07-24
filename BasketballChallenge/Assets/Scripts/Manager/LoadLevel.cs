using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    [SerializeField]
    [FormerlySerializedAs("NextLevelName")]
    private string nextLevelName;

    [SerializeField]
    [FormerlySerializedAs("Delay")]
    private float delay;

    public void LoadNextLevel()
    {
        base.StartCoroutine(this.LoadNextLevelRoutine());
    }

    private IEnumerator LoadNextLevelRoutine()
    {
        yield return new WaitForSeconds(this.delay);
        SceneManager.LoadScene(this.nextLevelName);
        yield break;
    }

}
