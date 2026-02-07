using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public RuntimeAnimatorController normalController;
    public RuntimeAnimatorController scaredController;
    bool isScared = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.powerMode)
        {
            animator.runtimeAnimatorController = scaredController;
            isScared = true;
        }
        else
        {
            animator.runtimeAnimatorController = normalController;
            isScared = false;
        }
    }
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
