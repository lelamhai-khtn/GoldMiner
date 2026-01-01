using System.Collections;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDownText;
    private float countDownTime = 60f;
    
    private void Start()
    {
        this.countDownText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(StartCountDown());
    }
    
    private IEnumerator StartCountDown()
    {
        float currentTime = countDownTime;
        
        while (currentTime > 0)
        {
            countDownText.text = Mathf.Ceil(currentTime).ToString();
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }
    }
}
