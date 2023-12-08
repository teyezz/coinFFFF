using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player���� �����Ǿ� �������� ������ �޾ƿ�
public class SkillManager : MonoBehaviour   //������ swapWeapon �ڸ�
{
    //���� ��ų ��� ��ī������ ��ų ţ���Ͽ� ����Ǵ� ������ �Ѱ輱�� ������ ����
    //Ű�Ҵ�, ũ�� �Ѱ�
    //�Ŀ� �̸� ����ġ�� �ؾ���
    public SKD[] usableSkill;

    protected Animator animator;
    protected AnimatorOverrideController AnimatorOverrideController;
    private GameObject slot;
    protected int skillIndex;

    protected AnimationClipOverrides clipOverrides;

    private void Start()
    {   //�迭�� �ޱ� �� �Է°� �迭 ���߱�
        
        slot = GameObject.Find("SkillQuickSlot");
        ReFreshQuickSlotSkill();
        animator = GetComponent<Animator>();

        skillIndex = 0;

        AnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = AnimatorOverrideController;

        //clipOverrides�� �ִϸ��̼� ������ ������ ����
        clipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(clipOverrides);
    }


    private void Update()
    {
    }
    //public void SetSkillAnimation() //refresh ���Ŀ� ����Ǿ� ��
    //{
    //    clipOverrides["Skill0"] = usableSkill[0].animationClip;
    //    clipOverrides["Skill1"] = usableSkill[1].animationClip;
    //    clipOverrides["Skill2"] = usableSkill[2].animationClip;
    //    clipOverrides["Skill3"] = usableSkill[3].animationClip;
    //    AnimatorOverrideController.ApplyOverrides(clipOverrides);
    //}
    public void ReFreshQuickSlotSkill() //��ų �ξؾƿ����� ����Ǿ� ��
    {
        usableSkill = slot.GetComponentsInChildren<SKD>();
        clipOverrides["Skill0"] = usableSkill[0]._animationClip;
        clipOverrides["Skill1"] = usableSkill[1]._animationClip;
        clipOverrides["Skill2"] = usableSkill[2]._animationClip;
        AnimatorOverrideController.ApplyOverrides(clipOverrides);
    }
}