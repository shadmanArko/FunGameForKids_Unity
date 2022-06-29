using UnityEngine;

public class Level5AnimationController : MonoBehaviour
{
    private Animator animator;
    private string RightAnswer = "Right";
    private string WrongAnswer = "Wrong";
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void onCorrectAnswer()
    {
        animator.SetTrigger(RightAnswer);
    }

    public void onWrongAnswer()
    {
        animator.SetTrigger(WrongAnswer);
    }
}
