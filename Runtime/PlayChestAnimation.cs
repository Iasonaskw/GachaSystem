using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayChestAnimation : MonoBehaviour
{
    //Script made to play the animation of the chest, used for the example, has only a visual purpose 
    [SerializeField] private Animator animator;
    [SerializeField] private Sprite chestSprite;
    public static PlayChestAnimation instance;
    private Image image;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        animator.enabled = false;
        image = GetComponent<Image>();
    }

    public void PlayAnimation()
    {
        StartCoroutine("PlayAndStopAnimation");
    }

    IEnumerator PlayAndStopAnimation()
    {
        animator.enabled = true;
        animator.Play("OpenNormalChestAnim", 0, 0f);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.enabled = false;

    }

    public void ResetChest()
    {
        image.overrideSprite = chestSprite;
    }
}
