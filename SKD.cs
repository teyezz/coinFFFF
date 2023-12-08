using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKD : MonoBehaviour
{
    public SkillDataScriptable skill;

    public ParticleSystem   _particleSystem;
    public float            _colldown;
    public Sprite           _sprite;
    public AnimationClip    _animationClip;

    void Init()
    {
        _particleSystem = skill.particleSystem;
        _colldown       = skill.colltime;
        _sprite         = skill.sprite;
        _animationClip = skill.animationClip;
    }
}
