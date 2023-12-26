using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;


//각 슬롯에 스킬이 할당된 지의 여부를 bool값으로 먼저 체크함
//Refresh는 스킬 드래그 앤 드랍에서 작동함
public class SkillListManage : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    private int index = 3;      //공란 배열의 숫자를 미리 정의함.
    [SerializeField]
    SkillQuickSlot[] usableSkill;          //animation과 start함수를 받아올 SKD를 배열로 만듬
    [SerializeField]
    protected Animator playerani;   //SKD에 들어 있는 AniMation을 불러와 플레이어에 입힘
                                    //스킬마다 다른 이팩트와 애니메이션 적용용
    private Transform playerTR;
    
    private float colldwon0;
    private float colldwon1;
    private float colldwon2;

    private bool isEmpty0;
    private bool isEmpty1;
    private bool isEmpty2;
    [SerializeField]               
    protected AnimatorOverrideController AnimatorOverrideController; //런타임 중 애니메이션을 오버라이드하기 위한 Controller
    [SerializeField]
    protected AnimationOverrideClip clipOverrides;                 //개별 애니메이션 클립을 받기 위한 애니메이션 클립
    //이걸 UI로 빼도 문제가 없음
    
    private void Start()
    {
        playerTR = GameObject.FindWithTag(Constant.Tag.Player).GetComponent<Transform>();
        playerani = playerTR.GetComponent<Animator>();
        usableSkill = new SkillQuickSlot[index];
        AnimatorOverrideController = new AnimatorOverrideController(playerani.runtimeAnimatorController);
        playerani.runtimeAnimatorController = AnimatorOverrideController;
        clipOverrides = new AnimationOverrideClip(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(clipOverrides);
        ReFreshAllSlot();
    }

    private void Update()
    {
        EmptySlotCheck();
        UpdateColldown();
        UpdateColldownSprite();
    }

    void UpdateColldown()
    {
        if (!isEmpty0)
        {
            colldwon0 = Mathf.Clamp(colldwon0, 0, usableSkill[0].EquipSkill._colldown);
            if (colldwon0 >= 0)
                colldwon0 -= Time.deltaTime;
        }
        if (!isEmpty1)
        {
            colldwon1 = Mathf.Clamp(colldwon1, 0, usableSkill[1].EquipSkill._colldown);
            if (colldwon1 >= 0)
                colldwon1 -= Time.deltaTime;
        }
        if (!isEmpty2)
        {
            colldwon2 = Mathf.Clamp(colldwon2, 0, usableSkill[2].EquipSkill._colldown);
            if (colldwon2 >= 0)
                colldwon2 -= Time.deltaTime;
        }
    }

    void EmptySlotCheck()
    {
        isEmpty0 = (usableSkill[0].EquipSkill == null) ? true : false;
        isEmpty1 = (usableSkill[1].EquipSkill == null) ? true : false;
        isEmpty2 = (usableSkill[2].EquipSkill == null) ? true : false;
    }
    void UpdateColldownSprite()
    {
        if(!isEmpty0)
            usableSkill[0].GetComponent<Image>().fillAmount = 1 - colldwon0/ usableSkill[0].EquipSkill._colldown;
        if (!isEmpty1)
            usableSkill[1].GetComponent<Image>().fillAmount = 1 - colldwon1 / usableSkill[1].EquipSkill._colldown;
        if (!isEmpty2)
            usableSkill[2].GetComponent<Image>().fillAmount = 1 - colldwon2 / usableSkill[2].EquipSkill._colldown;
    }
    public void ReFreshAllSlot()
    {
        for(int i = 0; i < index; i++)
        {
            if (transform.GetChild(i).GetComponent<SkillQuickSlot>() != null)
                usableSkill[i] = transform.GetChild(i).GetComponent<SkillQuickSlot>();
            else
                continue;
        }
        EmptySlotCheck();
        if (!isEmpty0)
            clipOverrides[Constant.AnimationClip.Skill0String] = usableSkill[0].EquipSkill._animationClip;  //받는 부분이 this[string]
        if (!isEmpty1)
            clipOverrides[Constant.AnimationClip.Skill1String] = usableSkill[1].EquipSkill._animationClip;  //받는 부분이 this[string]
        if (!isEmpty2)
            clipOverrides[Constant.AnimationClip.Skill2String] = usableSkill[2].EquipSkill._animationClip;  //받는 부분이 this[string]
        AnimatorOverrideController.ApplyOverrides(clipOverrides);   //AnimatorOverrideController는 이미 playerani에 속함. ApplyOverrides는 한 프레임 내 2개 이상의 Clip을 Override할 때만 사용한다
        playerani.runtimeAnimatorController = AnimatorOverrideController;
    }
    public void ReFresh(string name)
    {
        usableSkill = GetComponentsInChildren<SkillQuickSlot>();
        switch (name) 
        {
            case "QuickSkill0" :
                    clipOverrides[Constant.AnimationClip.Skill0String] = usableSkill[0].EquipSkill._animationClip;  //받는 부분이 this[string]
                    print("is Work1");
                break;
            case "QuickSkill1":
                    clipOverrides[Constant.AnimationClip.Skill1String] = usableSkill[1].EquipSkill._animationClip;  //받는 부분이 this[string]
                    print("is Work2");
                break;
            case "QuickSkill2":
                    clipOverrides[Constant.AnimationClip.Skill2String] = usableSkill[2].EquipSkill._animationClip;  //받는 부분이 this[string]
                    print("is Work3");
                break;
        }
        AnimatorOverrideController.ApplyOverrides(clipOverrides);   //AnimatorOverrideController는 이미 playerani에 속함. ApplyOverrides는 한 프레임 내 2개 이상의 Clip을 Override할 때만 사용한다
        playerani.runtimeAnimatorController = AnimatorOverrideController;
    }

    public void OnUse0(InputAction.CallbackContext context)
    {
        Vector3 SkillPos = playerTR.position + transform.forward * 4f + transform.up * 4f ;
        if (!isEmpty0 && colldwon0 <= 0)
        {
            if (usableSkill[0].EquipSkill.isProjectlie)
                Instantiate(usableSkill[0].EquipSkill.Projectlie, SkillPos, transform.rotation);
             
            playerani.Play(Constant.AnimationClip.Skill0String);
            colldwon0 = usableSkill[0].EquipSkill._colldown;
        }
        else
            print("Empty SkillSlot");
    }
    public void OnUse1(InputAction.CallbackContext context)
    {
        Vector3 SkillPos = playerTR.position + transform.forward * 4f + transform.up * 4f;
        if (!isEmpty1 && colldwon1 <= 0)
        {
            if (usableSkill[1].EquipSkill.isProjectlie)
                Instantiate(usableSkill[1].EquipSkill.Projectlie, SkillPos, transform.rotation);
        }
        else
            print("Empty SkillSlot");
    }
    public void OnUse2(InputAction.CallbackContext context)
    {
        Vector3 SkillPos = playerTR.position + transform.forward * 4f + transform.up * 4f;
        if (!isEmpty2 && colldwon2 <= 0)
        {
            if (usableSkill[2].EquipSkill.isProjectlie)
                Instantiate(usableSkill[2].EquipSkill.Projectlie, SkillPos, transform.rotation);
        }
        else
            print("Empty SkillSlot");
    }
}
