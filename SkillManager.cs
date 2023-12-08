using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player에게 부착되어 퀵슬롯의 정보를 받아옴
public class SkillManager : MonoBehaviour   //예시의 swapWeapon 자리
{
    //현재 스킬 사용 메카니즘은 스킬 큇슬록에 연결되는 정적인 한계선을 가지고 있음
    //키할당, 크기 한계
    //후에 이름 가지치기 해야함
    public SKD[] usableSkill;

    protected Animator animator;
    protected AnimatorOverrideController AnimatorOverrideController;
    private GameObject slot;
    protected int skillIndex;

    protected AnimationClipOverrides clipOverrides;

    private void Start()
    {   //배열로 받기 끝 입력과 배열 맞추기
        
        slot = GameObject.Find("SkillQuickSlot");
        ReFreshQuickSlotSkill();
        animator = GetComponent<Animator>();

        skillIndex = 0;

        AnimatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = AnimatorOverrideController;

        //clipOverrides가 애니메이션 정보를 가지고 있음
        clipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(clipOverrides);
    }


    private void Update()
    {
    }
    //public void SetSkillAnimation() //refresh 이후에 실행되야 함
    //{
    //    clipOverrides["Skill0"] = usableSkill[0].animationClip;
    //    clipOverrides["Skill1"] = usableSkill[1].animationClip;
    //    clipOverrides["Skill2"] = usableSkill[2].animationClip;
    //    clipOverrides["Skill3"] = usableSkill[3].animationClip;
    //    AnimatorOverrideController.ApplyOverrides(clipOverrides);
    //}
    public void ReFreshQuickSlotSkill() //스킬 인앤아웃마다 실행되야 함
    {
        usableSkill = slot.GetComponentsInChildren<SKD>();
        clipOverrides["Skill0"] = usableSkill[0]._animationClip;
        clipOverrides["Skill1"] = usableSkill[1]._animationClip;
        clipOverrides["Skill2"] = usableSkill[2]._animationClip;
        AnimatorOverrideController.ApplyOverrides(clipOverrides);
    }
}