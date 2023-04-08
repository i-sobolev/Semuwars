using UnityEngine;

public class CharacterAnimatorBehavior : StateMachineBehaviour
{
    private string _currentFallingBlocker = string.Empty;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsCurrentStateEquals(stateInfo, "Jump"))
        {
            animator.SetBool("FallingBlocked", true);
        }

        if (IsCurrentStateEquals(stateInfo, "KunaiFalling"))
        {
            animator.SetBool("FallingBlocked", true);
        }

        if (IsCurrentStateEquals(stateInfo, "SwordFalling"))
        {
            animator.SetBool("FallingBlocked", true);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (IsCurrentStateEquals(stateInfo, _currentFallingBlocker))
        {
            animator.SetBool("FallingBlocked", false);
        }
    }

    public bool IsCurrentStateEquals(AnimatorStateInfo info, string name)
    {
        var result = info.shortNameHash == Animator.StringToHash(name);

        if (result)
            _currentFallingBlocker = name;

        return result;
    }
}
