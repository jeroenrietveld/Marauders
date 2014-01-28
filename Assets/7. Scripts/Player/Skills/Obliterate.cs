using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Obliterate : SkillBase
{
    private float _range = 3.5f;
    private float _damage = .1f;
    private float _force = 200f;
    private float _duration = 1.625f;
    private Timer _obliterate;

    private Dictionary<PlayerIndex, int> hitAmounts = new Dictionary<PlayerIndex, int>();

    private static AnimationHandler.AnimationSettings _animationSettingsOneHanded = new AnimationHandler.AnimationSettings(
        new AttackInfo("Obliterate_1handed", 1.0f, -1f),
        AnimationHandler.MixTransforms.Upperbody | AnimationHandler.MixTransforms.Lowerbody,
        3,
        WrapMode.Once
        );

    private static AnimationHandler.AnimationSettings _animationSettingsTwoHanded = new AnimationHandler.AnimationSettings(
        new AttackInfo("Obliterate_2handed", 1.0f, -1f),
        AnimationHandler.MixTransforms.Upperbody | AnimationHandler.MixTransforms.Lowerbody,
        3,
        WrapMode.Once
        );

    public Obliterate()
        : base(3, _animationSettingsOneHanded, _animationSettingsTwoHanded)
    {
        foreach (Player player in GameManager.Instance.playerRefs)
        {
            hitAmounts.Add(player.index, 0);
        }

        _obliterate = new Timer(_duration);

        _obliterate.AddCallback(0f, delegate {
            // Set the correct animation to use via the animation name and the current weapong prototype.
            string name = "Obliterate_" + this.gameObject.GetComponent<Attack>().weapon.weaponType;
            animationSettings = allAnimationSettings.First(x => x.attackInfo.animationName == name);
        });

        _obliterate.AddCallback(delegate
        {
            // When the animation has ended reset all of the hit amounts.
            foreach (Player pl in GameManager.Instance.playerRefs)
            {
                hitAmounts[pl.index] = 0;
            }
        });
    }

    
    public override void PerformAction()
    {
        if (!cooldown.running)
        {
            animation[animationSettings.attackInfo.animationName].weight = 0.8f;
            //animation.Blend(animationSettings.attackInfo.animationName, 0.8f);
            animation.CrossFade(animationSettings.attackInfo.animationName, 0.3f);
            cooldown.Start();
            OnPerformAction();
        }
    }
    
    protected override void OnPerformAction()
    {
        _obliterate.Start();
    }

    protected override void OnUpdate()
    {
        _obliterate.Update();
        if(_obliterate.running)
        {
            Avatar attacker = this.gameObject.GetComponent<Avatar>();

            foreach (var player in GameManager.Instance.playerRefs)
            {
                // Make sure that we can only hit the player maximum of two times. Since the animation
                // spins around two times.
                if(player.index != attacker.player.index && hitAmounts[player.index] < 2)
                {
                    var avatar = player.avatar;
                    float distance = Vector3.Distance(this.transform.position, avatar.transform.position);

                    if (distance <= _range)
                    {
                        hitAmounts[player.index]++;

                        var heartbeat = avatar.GetComponentInChildren<Heartbeat>();

                        var direction = transform.position - avatar.transform.position;
                        direction.y = 0;
                        direction.Normalize();

                        var source = new DamageSource(
                            GetComponent<Avatar>().player,
                            direction,
                            -direction * _force,
                            _damage,
                            0,
                            false
                            );
                        heartbeat.DoAttack(source);
                    }
                }
            }
        }
    }
}
