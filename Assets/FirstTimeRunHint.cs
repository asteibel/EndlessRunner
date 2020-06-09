using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstTimeRunHint : MonoBehaviour
{

    public TextMeshProUGUI hint;

    private int numberOfTimesToShowHint = 2;

    private void Awake()
    {
        Debug.Log("AWAKE FirstTimeRun");
    }

    private void Start()
    {
        if (GameManager.instance.gameData.numberOfRuns > numberOfTimesToShowHint)
        {
            hint.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void FadeOut()
    {
        if (GameManager.instance.gameData.numberOfRuns <= numberOfTimesToShowHint)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);

        float counter = 0f;

        while (counter < 2f)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, counter / 2f);

            hint.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
    }

    public void Reset()
    {
        if (GameManager.instance.gameData.numberOfRuns <= numberOfTimesToShowHint)
        {
            hint.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
