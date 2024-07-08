using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEssentials : MonoBehaviour
{
    [SerializeField] private string exitTriggerName = "Exit";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void LoadScene(int buildIndex)
    {
        if (_animator) StartCoroutine(LoadSceneCoroutine(buildIndex));
        else SceneManager.LoadSceneAsync(buildIndex);
    }

    public void RestartScene()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator LoadSceneCoroutine(int buildIndex)
    {
        _animator.SetTrigger(exitTriggerName);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
