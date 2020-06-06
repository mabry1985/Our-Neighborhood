using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionProgressBar : MonoBehaviour
{

    private Slider slider;
    public float fillSpeed = 0.5f;
    //public float duration = 5f;
    private float targetProgress = 0;

    private void Awake() {
        slider = gameObject.GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(IncrementProgress());
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value < targetProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

    // public void IncrementProgress(float newProgress)
    // {
    //     targetProgress = slider.value + newProgress;
    // }

    public IEnumerator IncrementProgress(float duration)
    {
        float time = 0.0f;
        while (time < duration)
        {
            slider.value = time / Mathf.Max(duration, 0.1f);
            time += Time.deltaTime;

            if(time >= duration)
                gameObject.SetActive(false);
                
            yield return new WaitForEndOfFrame();
        }
    }
}
