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

    public void ReloadCurrentLevel()
    {
        base.StartCoroutine(this.ReloadCurrentLevelRoutine());
    }

    private IEnumerator LoadNextLevelRoutine()
    {
        yield return new WaitForSeconds(this.delay);
        SceneManager.LoadScene(this.nextLevelName, LoadSceneMode.Single);
        yield break;
    }

    private IEnumerator ReloadCurrentLevelRoutine()
    {
        yield return new WaitForSeconds(this.delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        yield break;
    }

}
