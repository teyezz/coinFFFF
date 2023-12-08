using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOverrideClip : List<KeyValuePair<AnimationClip, AnimationClip>>
{                                   //��ųʸ��� ������̺� -> �������� ������ �ȵȴ�
    public AnimationOverrideClip(int capacity) : base(capacity) { }

    public AnimationClip this[string name] //<=���⿡ �ִϸ��̼� �̸��� �� ����
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