using UnityEngine;
using System.Collections.Generic;

public class Bulwark : SkillBase
{
    private Timer _bulwark;

    private float _duration = 4.5f;
    private float _factorMoveSpeedReduction = 0.5f;
    private float _factorDamageReduction = 0.5f;

    private static AnimationHandler.AnimationSettings _animationSettingsStart = new AnimationHandler.AnimationSettings(
        new AttackInfo("Bulwark-start", 1.0f, -1f),
        AnimationHandler.MixTransforms.Upperbody | AnimationHandler.MixTransforms.Lowerbody,
        3,
        WrapMode.ClampForever
        );

    private static AnimationHandler.AnimationSettings _animationSettingsEnd = new AnimationHandler.AnimationSettings(
        new AttackInfo("Bulwark-end", 1.0f, -1f),
        AnimationHandler.MixTransforms.Upperbody | AnimationHandler.MixTransforms.Lowerbody,
        3,
        WrapMode.Once
        );

    private DecoratableFloat _movementSpeedReduction;

    public Bulwark()
        : base(6, _animationSettingsStart, _animationSettingsEnd)
    {           
        _bulwark = new Timer(_duration);
        _bulwark.AddCallback(0f, delegate
        {
            _movementSpeedReduction = GetComponent<Movement>().movementSpeed;
            _movementSpeedReduction.filters += ModulateDRMovement;
        });

        _bulwark.AddCallback(2.6f, delegate
        {
            // Start animation has ended. Switch to the end animation.
            animationSettings = _animationSettingsEnd;
            animation.Stop(_animationSettingsStart.attackInfo.animationName);
            animation[animationSettings.attackInfo.animationName].weight = 0.6f;
            animation.Blend(animationSettings.attackInfo.animationName, 0.6f, 0.2f);
        });

        _bulwark.AddCallback(delegate
        {
            _movementSpeedReduction.filters -= ModulateDRMovement;
            // Reset the animation to the start animation.
            animationSettings = _animationSettingsStart;
            animation[animationSettings.attackInfo.animationName].weight = 0.6f;
        });
    }

    public override void PerformAction()
    {
        if (!cooldown.running)
        {
            animationSettings = _animationSettingsStart;
            animation[animationSettings.attackInfo.animationName].weight = 0.6f;
            animation.Blend(animationSettings.attackInfo.animationName, 0.6f);

            cooldown.Start();
            OnPerformAction();
        }
    }

    float ModulateDRMovement(float dr)
    {
        return dr * _factorMoveSpeedReduction;
    }

    float ModulateDRDamage(float dr)
    {
        return dr * _factorDamageReduction;
    }

    protected override void OnPerformAction()
    {
        _bulwark.Start();
    }

    protected override void OnUpdate()
    {
        _bulwark.Update();
    }
}
