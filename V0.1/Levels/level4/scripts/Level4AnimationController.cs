using UnityEngine;

public class Level4AnimationController : Singleton<Level4AnimationController>
{
    [SerializeField] private Animator animator;
    private string RightAnswer = "Right";
    private string WrongAnswer = "Wrong";

    public void onCorrectAnswer()
    {
        animator.SetTrigger(RightAnswer);
    }

    public void onWrongAnswer()
    {
        animator.SetTrigger(WrongAnswer);
    }
}
