using UnityEngine;

public class Level10AnimationController : MonoBehaviour
{
    private Animator _animator;

    private string _rightAnswer = "Right";
    private string _wrongAnswer = "Wrong";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void onCorrectAnswer()
    {
        _animator.SetTrigger(_rightAnswer);
    }

    public void onWrongAnswer()
    {
        _animator.SetTrigger(_wrongAnswer);
    }
}
