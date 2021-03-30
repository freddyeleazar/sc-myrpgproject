using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
//using UnityEditor.Animations;
using UnityEngine;

public class NpcIngame_ : UnitIngame_
{
    //#region Animator Components
    //private AnimatorState speedAnimator;//Speed Animator
    //private BlendTree speedBlend;//Speed Blend
    //private BlendTree idleAngleBlend;//Idle Angle Blend
    //private BlendTree walkAngleBlend;//Walk Angle Blend
    //private BlendTree runAngleBlend;//Run Angle Blend
    //private AnimatorState attackAnimator;//Attack Animator
    //private BlendTree attackAngleBlend;//Attack Angle Blend
    //private AnimatorState useBowAnimator;//Use Bow Animator
    //private BlendTree useBowAngleBlend;//Use Bow Angle Blend
    //private AnimatorState dieAnimator;//Die Animator
    //private BlendTree dieAngleBlend;//Die Angle Blend
    //#endregion

    public override void GenerateAnimationClips()
    {
        base.GenerateAnimationClips();
        //SetChildrenMotionSpeed(walkAngleBlend, 10);
        //SetChildrenMotionSpeed(runAngleBlend, 10);
        //SetChildrenMotionSpeed(attackAngleBlend, 10);
        //SetChildrenMotionSpeed(useBowAngleBlend, 10);
        //SetChildrenMotionSpeed(dieAngleBlend, 10);
    }

    public override void AddToAnimatorController(AnimationClip animationClip, AnimatedAction animatedAction)
    {
        base.AddToAnimatorController(animationClip, animatedAction);
        switch (animatedAction)
        {
            case AnimatedAction.Idle:
                //idleAngleBlend.AddChild(animationClip);
                break;
            case AnimatedAction.Walk:
                //walkAngleBlend.AddChild(animationClip);
                break;
            case AnimatedAction.Run:
                //runAngleBlend.AddChild(animationClip);
                break;
            case AnimatedAction.Attack:
                //attackAngleBlend.AddChild(animationClip);
                break;
            case AnimatedAction.UseBow:
                //useBowAngleBlend.AddChild(animationClip);
                break;
            case AnimatedAction.Die:
                //dieAngleBlend.AddChild(animationClip);
                break;
            default:
                break;
        }
    }

    public override void SetAnimator()
    {
        base.SetAnimator();
        //Los componentes de animator que siguen a continuación sólo corresponden a objetos que tengan la lógica de animación del Player
        //speedAnimator = rootStateMachine.states[0].state;//Speed Animator
        //speedBlend = (BlendTree)speedAnimator.motion;//Speed Blend
        //idleAngleBlend = (BlendTree)speedBlend.children[0].motion;//Idle Angle Blend
        //walkAngleBlend = (BlendTree)speedBlend.children[1].motion;//Walk Angle Blend
        //runAngleBlend = (BlendTree)speedBlend.children[2].motion;//Run Angle Blend

        //attackAnimator = rootStateMachine.states.ToList().Find(t => t.state.name == "Attack Animator").state;//Attack Animator
        //attackAngleBlend = (BlendTree)attackAnimator.motion;//Attack Angle Blend
        //useBowAnimator = rootStateMachine.states.ToList().Find(t => t.state.name == "Use Bow Animator").state;//Use Bow Animator
        //useBowAngleBlend = (BlendTree)useBowAnimator.motion;//Use Bow Angle Blend
        //dieAnimator = rootStateMachine.states.ToList().Find(t => t.state.name == "Die Animator").state;//Die Animator
        //dieAngleBlend = (BlendTree)dieAnimator.motion;//Die Angle Blend
    }

    //public void SetChildrenMotionSpeed(BlendTree blendTree, float animationSpeed)
    //{
    //    var serialized = new SerializedObject(blendTree);
    //    var children = serialized.FindProperty("m_Childs");
    //    for (int i = 0; i < children.arraySize; i++)
    //    {
    //        var child = children.GetArrayElementAtIndex(i);
    //        var timeScale = child.FindPropertyRelative("m_TimeScale");
    //        timeScale.floatValue = animationSpeed;
    //    }
    //    serialized.ApplyModifiedProperties();
    //    walkAngleBlend = (BlendTree)serialized.targetObject;
    //}

    //public override void EmptyAnimator()
    //{
    //    base.EmptyAnimator();
    //    ChildMotion[] empty = new ChildMotion[0];
    //    idleAngleBlend.children = empty;
    //    walkAngleBlend.children = empty;
    //    runAngleBlend.children = empty;
    //    attackAngleBlend.children = empty;
    //    useBowAngleBlend.children = empty;
    //    dieAngleBlend.children = empty;
    //}
}
