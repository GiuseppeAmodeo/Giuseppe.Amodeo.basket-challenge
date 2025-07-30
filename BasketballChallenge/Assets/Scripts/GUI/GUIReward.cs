using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUIReward : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjectsImgStar;

    private void Reset()
    {
        RectTransform[] componentsInChildren = GetComponentsInChildren<RectTransform>(true);

        GameObject gameObject = componentsInChildren.First((RectTransform hR) => hR.name.Contains("FirstStar")).gameObject;
        GameObject gameObject2 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("SecondStar")).gameObject;
        GameObject gameObject3 = componentsInChildren.First((RectTransform hR) => hR.name.Contains("ThirdStar")).gameObject;

        this.gameObjectsImgStar = new GameObject[]
        {
             gameObject,
             gameObject2,
             gameObject3
        };
    }

    private void Awake()
    {
        for (int i = 0; i < this.gameObjectsImgStar.Length; i++)
        {
            this.gameObjectsImgStar[i].SetActive(false);
        }
    }

    public void SetRewardStars(int score)
    {
        int stars = 0;

        if (score >= 35)
        {
            stars = 3;
        }
        else if (score >= 25)
        {
            stars = 2;
        }
        else if (score >= 10)
        {
            stars = 1;
        }

       StartCoroutine(this.ActiveStar(stars));
    }

    private IEnumerator ActiveStar(int currentStar)
    {
        for (int i = 0; i < this.gameObjectsImgStar.Length; i++)
        {
            this.gameObjectsImgStar[i].SetActive(i < currentStar);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
