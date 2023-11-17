using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image image;
    [SerializeField] Text countDownText;
    private float fps;
    private float countDownTime;
    public void StartCountDown()
    {
        Debug.Log("Count down");
        countDownTime = 4f;
        StartCoroutine(AnimSequence());
        StartCoroutine(CountDownTime());
    }
    // Update is called once per frame
    private IEnumerator AnimSequence()
    {
        var delay = new WaitForSeconds(countDownTime/sprites.Length);
        int index = 0;
        while (true)
        {
            if (index >= sprites.Length) index = 0;
            ShowFrame(index);
            index++;
            yield return delay;
        }
    }
    private IEnumerator CountDownTime()
    {
        while (countDownTime >= 0)
        {
            countDownText.text = countDownTime.ToString();
            countDownTime -= 1;
            yield return new WaitForSeconds(1f);
        }
    }
    void ShowFrame(int index)
    {
        image.sprite = sprites[index];
    }
}
