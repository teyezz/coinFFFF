using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEditor.Timeline;

public class SkillListManage : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    private int index = 3;      //공란 배열의 숫자를 미리 정의함.
    [SerializeField]
    SKD[] usableSkill;          //animation과 start함수를 받아올 SKD를 배열로 만듬
    [SerializeField]
    protected Animator playerani;   //SKD에 들어 있는 AniMation을 불러와 플레이어에 입힘
                                    //스킬마다 다른 이팩트와 애니메이션 적용용
    [SerializeField]               
    protected AnimatorOverrideController AnimatorOverrideController; //런타임 중 애니메이션을 오버라이드하기 위한 Controller
    [SerializeField]
    protected AnimationOverrideClip clipOverrides;                 //개별 애니메이션 클립을 받기 위한 애니메이션 클립
    
    private void Start()
    {
        playerani = GetComponentInParent<Animator>();
        usableSkill = new SKD[index];
        AnimatorOverrideController = new AnimatorOverrideController(playerani.runtimeAnimatorController);
        playerani.runtimeAnimatorController = AnimatorOverrideController;
        clipOverrides = new AnimationOverrideClip(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(clipOverrides);
        ReFresh();
       
    }


    public void ReFresh()  // 밑에 거가 추가가 안됨
    {
        Array.Clear(usableSkill,0,usableSkill.Length);
        usableSkill = GetComponentsInChildren<SKD>();
        clipOverrides[Constant.AnimationClip.Skill0String] = usableSkill[0]._animationClip;  //받는 부분이 this[string]
        print("is Work1");
        clipOverrides[Constant.AnimationClip.Skill1String] = usableSkill[1]._animationClip;  //받는 부분이 this[string]
        print("is Work2");
        clipOverrides[Constant.AnimationClip.Skill2String] = usableSkill[2]._animationClip;  //받는 부분이 this[string]
        print("is Work3");
        AnimatorOverrideController.ApplyOverrides(clipOverrides);   //AnimatorOverrideController는 이미 playerani에 속함. ApplyOverrides는 한 프레임 내 2개 이상의 Clip을 Override할 때만 사용한다
        playerani.runtimeAnimatorController = AnimatorOverrideController;
    }

    public void OnUse0(InputAction.CallbackContext context)
    {
        Vector3 SkillPos = transform.position + transform.forward * 4f + transform.up * 4f ;
        if (context.started)
        {
            if (usableSkill[0].isProjectlie)
                Instantiate(usableSkill[0].Projectlie, SkillPos, transform.rotation);
            playerani.Play(Constant.AnimationClip.Skill0String);
        }
    }
    public void OnUse1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var skill = Instantiate(usableSkill[1]);
            playerani.Play(Constant.AnimationClip.Skill1String);
        }
    }
    public void OnUse2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            var skill = Instantiate(usableSkill[2]);
            playerani.Play(Constant.AnimationClip.Skill2String);
        }
    }
}
