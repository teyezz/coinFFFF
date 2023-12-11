using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class SkillListManage : MonoBehaviour
{
    
    [SerializeField, Range(1, 8)]
    private int index =3;
    [SerializeField]
    SKD[] usableSkill;     //<== 얘가 안들어감 진짜 초 비상
    protected Animator playerani;
    protected AnimatorOverrideController AnimatorOverrideController;
    protected AnimationClipOverrides clipOverrides;

    readonly string skill0 = "Skill0";
    readonly string skill1 = "Skill1";
    readonly string skill2 = "Skill2";
    
    private void Start()
    {
        playerani = GetComponentInParent<Animator>();
        usableSkill = new SKD[index];
        usableSkill = GetComponentsInChildren<SKD>();
        AnimatorOverrideController = new AnimatorOverrideController(playerani.runtimeAnimatorController);
        playerani.runtimeAnimatorController = AnimatorOverrideController;
        
        clipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        ReFresh();
        AnimatorOverrideController.GetOverrides(clipOverrides);
       
    }

    void ReFresh()  // 밑에 거가 추가가 안됨
    {
        clipOverrides["Skill0"] = usableSkill[0]._animationClip;
        clipOverrides["Skill1"] = usableSkill[1]._animationClip;
        clipOverrides["Skill2"] = usableSkill[2]._animationClip;
        AnimatorOverrideController.ApplyOverrides(clipOverrides);
    }

    void OnUse0()
    {
        print("OnUse0");
        var skill = Instantiate(usableSkill[0].gameObject, this.transform.position, this.transform.rotation);
        playerani.Play(skill0);
    }
    void OnUse1()
    {
        var skill = Instantiate(usableSkill[1].gameObject, this.transform.position, this.transform.rotation);
        playerani.Play(skill1);
    }
    void OnUse2()
    {
        var skill = Instantiate(usableSkill[2].gameObject, this.transform.position, this.transform.rotation);
        playerani.Play(skill2);
    }
}