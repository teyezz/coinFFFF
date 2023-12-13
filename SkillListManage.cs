using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

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
    protected AnimationClipOverrides clipOverrides;                 //개별 애니메이션 클립을 받기 위한 애니메이션 클립

    readonly int skill0 = Animator.StringToHash("Skill0");      //개별 스킬들의 상수 string을 int로 저장
    readonly int skill1 = Animator.StringToHash("Skill1");
    readonly int skill2 = Animator.StringToHash("Skill2");
    
    private void Start()
    {
        playerani = GetComponentInParent<Animator>();
        usableSkill = new SKD[index];
        AnimatorOverrideController = new AnimatorOverrideController(playerani.runtimeAnimatorController);
        playerani.runtimeAnimatorController = AnimatorOverrideController;
        clipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(clipOverrides);
        ReFresh();
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))      //후에 inputSystem으로 개선한다
            ReFresh();
        if (Input.GetKeyDown(KeyCode.Z))
            OnUse0();
        if (Input.GetKeyDown(KeyCode.X))
            OnUse1();
        if (Input.GetKeyDown(KeyCode.C))
            OnUse2();
    }
    void ReFresh()  // 밑에 거가 추가가 안됨
    {
        Array.Clear(usableSkill,0,usableSkill.Length);
        usableSkill = GetComponentsInChildren<SKD>();
        clipOverrides["Skill0"] = usableSkill[0]._animationClip;  //받는 부분이 this[string]
        print("is Work1");
        clipOverrides["Skill1"] = usableSkill[1]._animationClip;  //받는 부분이 this[string]
        print("is Work2");
        clipOverrides["Skill2"] = usableSkill[2]._animationClip;  //받는 부분이 this[string]
        print("is Work3");
        AnimatorOverrideController.ApplyOverrides(clipOverrides);   //AnimatorOverrideController는 이미 playerani에 속함. ApplyOverrides는 한 프레임 내 2개 이상의 Clip을 Override할 때만 사용한다
        playerani.runtimeAnimatorController = AnimatorOverrideController;
    }

    void OnUse0()
    {
        print("OnUse0");
        var skill = Instantiate(usableSkill[0].gameObject, playerani.transform);
        Destroy(skill, usableSkill[0]._colldown);
        playerani.Play("Skill0");
        playerani.SetBool("IsSkill", true);
    }
    void OnUse1()
    {
        print("OnUse1");
        var skill = Instantiate(usableSkill[1]);
        playerani.Play("Skill1");
    }
    void OnUse2()
    {
        print("OnUse2");
        var skill = Instantiate(usableSkill[2]);
        playerani.Play("Skill2");
    }
}
