using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour, IButtonAction
{
    public float activationTime = 3f; // 버튼 활성화에 필요한 시간
    public Slider activationSlider;
    private bool isActivated = false;
    private float timer = 0f;
    private Coroutine activationCoroutine;
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();

        if (activationSlider != null)

        {
            activationSlider.maxValue = activationTime;
            activationSlider.value = 0f;            
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated && button != null && button.interactable)
        {
            if (activationSlider != null)
            {
                activationSlider.gameObject.SetActive(true); 
            }
            activationCoroutine = StartCoroutine(ActivateButton()); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activationCoroutine != null)
            {
                StopCoroutine(activationCoroutine); 
                activationCoroutine = null;
            }

            timer = 0f;

            isActivated = false;

            if (activationSlider != null)
            {
                activationSlider.value = 0f;
                activationSlider.gameObject.SetActive(false); 
            }
        }
    }

    private IEnumerator ActivateButton()
    {
        while (timer < activationTime)
        {
            timer += Time.deltaTime;
            if (activationSlider != null)
            {
                activationSlider.value = timer;
            }
            yield return null;
        }
        isActivated = true;

        OnButtonClick();

        if (activationSlider != null)
        {
            activationSlider.gameObject.SetActive(false); 
        }

        ResetButton();
    }

    protected virtual void OnButtonClick()
    {
        if (button != null && button.interactable)
        {
            Execute();
        }
    }

    public virtual void Execute()
    {
        
    }

    private void ResetButton()
    {
        isActivated = false;
        timer = 0f;
        if (activationSlider != null)
        {
            activationSlider.value = 0f;
            activationSlider.gameObject.SetActive(false); 
        }
    }
}