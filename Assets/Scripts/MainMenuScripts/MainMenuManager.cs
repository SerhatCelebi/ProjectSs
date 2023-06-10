using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject difficultyScreen;
    [SerializeField] GameObject optionsScreen;

    [SerializeField] Slider slider;

    [SerializeField] private Vector3 endTransform = new Vector3(0f, 0f, 0f), startTransform = new Vector3(0f, -960f, 0f);
    [SerializeField] private float LerpValue = 0f;
    
    public void EasyGameButton()
    {
        SceneManager.LoadScene(1);
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Stop("Theme");
    }
    public void MediumGameButton()
    {
        SceneManager.LoadScene(2);
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Stop("Theme");
    }
    public void HardGameButton()
    {
        SceneManager.LoadScene(3);
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Stop("Theme");
    }

    public void ApplyButton()
    {
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.SetMasterVolume(slider.value);
    }

    public void OptionsButton()
    {
        AudioManager.Instance.Play("Click");
        optionsScreen.SetActive(true);
    }
    public void CloseOptionsButton()
    {
        AudioManager.Instance.Play("Click");
        optionsScreen?.SetActive(false);
    }
    public void AgainstTimeButton()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(SetOnDifficultyScreen(startTransform, endTransform, 1f));
        AudioManager.Instance.Play("Click");
    }
    public void CloseDifficultyButton()
    {
        StartCoroutine(SetOffDifficultyScreen(endTransform, startTransform, 1f));
        AudioManager.Instance.Play("Click");
    }

    IEnumerator SetOnDifficultyScreen(Vector3 v_start, Vector3 v_end, float v_duration)
    {
        difficultyScreen.SetActive(true);
        float elapsed = 0f;
        while(elapsed < v_duration)
        {
            LerpValue = Mathf.Lerp(0f, 1f, elapsed / v_duration);
            difficultyScreen.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(v_start, v_end, LerpValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        difficultyScreen.GetComponent<RectTransform>().anchoredPosition = endTransform;
        yield return null;
    }

    IEnumerator SetOffDifficultyScreen(Vector3 v_start, Vector3 v_end, float v_duration)
    {
        float elapsed = 0f;
        while (elapsed < v_duration)
        {
            LerpValue = Mathf.Lerp(0f, 1f, elapsed / v_duration);
            difficultyScreen.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(v_start, v_end, LerpValue);
            elapsed += Time.deltaTime;
            yield return null;
        }
        difficultyScreen.GetComponent<RectTransform>().anchoredPosition = startTransform;
        difficultyScreen.SetActive(false);
        yield return null;
    }
}
