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
    public void ReFreshQuickSlotSkill() //퀵슬롯에서 등록된 스킬이 변경될 때마다 콜백함
    {
        usableSkill = slot.GetComponentsInChildren<SKD>();
        clipOverrides["Skill0"] = usableSkill[0]._animationClip;
        clipOverrides["Skill1"] = usableSkill[1]._animationClip;
        clipOverrides["Skill2"] = usableSkill[2]._animationClip;
        AnimatorOverrideController.ApplyOverrides(clipOverrides);
    }
    /*
     * 스킬 퀵슬롯은 3개이고 플레이어는 각각 A, B, C, D, capoera라는 스킬을 가지고 있다.
     * 각각의 스킬들은 스킬의 정보가 담긴 SKD를 가지고 있고
     * 스킬의 작동을 포함한, 오브젝트 이름과 같은 클래스를 가지고 있다
     * ex A => SKD class, A class; B => SKD class, B class; capoera => SKD class, capoera class;
     */
    private void UseSkill()
    {   
        switch (KeyCode)        //입력받은 키에 따라 작동. 스킬슬롯은 각 1.Q 2.W 3.E를 
        {
            case Q:
                {       //예상되는 문제. 스킬의 작동부를 가져 오려면
                        //존재하는 모든 스킬 컴포넌트가 있는지 확인해야 함
                        //모든 스킬의 상세 작동이 다르므로 이런 식의 개별 스크립트를 만들어야 함
                    if(usableSkill[0].GetComponent<A>() != null)
                        usableSkill[0].GetComponent<A>().UseSkill();
                    
                    else if(usableSkill[0].GetComponent<B>() != null)
                        usableSkill[0].GetComponent<B>().UseSkill();
                    
                    else if(usableSkill[0].GetComponent<C>() != null)
                        usableSkill[0].GetComponent<C>().UseSkill;
                    
                    else if(usableSkill[0].GetComponent<D>() != null)
                        usableSkill[0].GetComponent<D>().UseSkill;
                    else if(usableSkill[0].GetComponent<capoera>() != null)
                        usableSkill[0].GetComponent<capoera>().UseSkill;
                }
            case W:
                {
                    if (usableSkill[1].GetComponent<A>() != null)
                        usableSkill[1].GetComponent<A>().UseSkill();

                    else if (usableSkill[1].GetComponent<B>() != null)
                        usableSkill[1].GetComponent<B>().UseSkill();

                    else if (usableSkill[1].GetComponent<C>() != null)
                        usableSkill[1].GetComponent<C>().UseSkill;

                    else if (usableSkill[1].GetComponent<D>() != null)
                        usableSkill[1].GetComponent<D>().UseSkill;
                    else if (usableSkill[1].GetComponent<capoera>() != null)
                        usableSkill[1].GetComponent<capoera>().UseSkill;
                }
                //~~이런 식으로 할당된 키마다 전부 확인
                //퀵슬롯 1개가 늘어나면 존재하는 스킬의 수 N만큼 확인이 늘어남
                //스킬 1개가 늘어나면 퀵슬롯 수 S만큼 확인 절차가 늘어남
                // == 지옥
        }
    }
}
