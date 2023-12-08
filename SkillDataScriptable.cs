using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skilldata" , menuName = "Skill/skill", order = int.MaxValue)]
public class SkillDataScriptable : ScriptableObject
{
    public AnimationClip animationClip;
    public Sprite sprite;
    public float colltime;
    public ParticleSystem particleSystem;
}