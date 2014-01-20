using UnityEngine;
using System.Collections;

public class SkillCooldown : MonoBehaviour {

	private SkillBase _skill;
	public SkillType skillType;

	// Use this for initialization
	void Start () {
		foreach(var skill in transform.root.GetComponents<SkillBase>())
		{
			if(skill.skillType == skillType)
			{
				_skill = skill;
				break;
			}
		}

		if(_skill != null)
		{
			_skill.cooldown.AddTickCallback(delegate
			{
				renderer.material.SetFloat("_phase", _skill.cooldown.Phase());
			});
		}
	}
}
