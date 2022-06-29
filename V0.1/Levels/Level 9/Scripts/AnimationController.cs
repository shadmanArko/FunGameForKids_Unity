using UnityEngine;

public class AnimationController : Singleton<AnimationController>
{
    [SerializeField] private Animator animator;
    [SerializeField] public Animator OwlWrongAnimator;
    [SerializeField] public Animator OwlWinAnimator;
    [SerializeField] private string ClickAnim = "IsClicked";
    private string _owlCorrectAnimationString = "IsWin";
    private string _owlWrongAnimationString = "IsHit";
    private bool _state = false;

    void OnClickAnimation()
    {
        _state = !_state;
        animator.SetBool(ClickAnim, _state);
    }

    public void OnCorrectAnimation()
    {
        OwlWinAnimator.SetTrigger(_owlCorrectAnimationString);
    }

    public void OnWrongAnimation()
    {
        OwlWrongAnimator.SetTrigger(_owlWrongAnimationString);

    }
}
