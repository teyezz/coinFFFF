using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrideClip : List<KeyValuePair<AnimationClip, AnimationClip>>
{                                   //딕셔너리가 헤시테이블 -> 순차성이 보장이 안된다
    public AnimationOverrideClip(int capacity) : base(capacity) { }

    public AnimationClip this[string name] //<=여기에 애니메이션 이름이 들어갈 예정
    {
        get { return this.Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = this.FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
        }
    }
}