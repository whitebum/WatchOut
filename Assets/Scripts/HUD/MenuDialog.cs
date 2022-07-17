using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDialog : MonoBehaviour
{
    [SerializeField] private Text prompt;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isActive = false;
    private void Reset()
    {
        if (!prompt)
            prompt = transform.Find("Text").TryGetComponent<Text>(out var text) ? text : null;
    }

    public void SetPrompt(string text)
    {
        prompt.text = $" {text}";
    }

    public void ActiveDialog(string text)
    {
        animator.SetBool(nameof(isActive), isActive = true);
    }

    public void DeactiveDialog(string text)
    {
        animator.SetBool(nameof(isActive), isActive = false);
    }
}
