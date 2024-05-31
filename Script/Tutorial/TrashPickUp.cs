using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashPickUp : DragDropItem
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private ParticleEffect particleEffect;

#if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();

        if (!tutorialManager)
            tutorialManager = FindObjectOfType<TutorialManager>();

        if (!particleEffect)
            particleEffect = FindObjectOfType<ParticleEffect>();
    }
#endif

    public override void OnPointerDown(PointerEventData eventData)
    {
        tutorialManager.IncreasedPoint();

        if (!particleEffect)
            particleEffect = FindObjectOfType<ParticleEffect>();

        if (particleEffect)
            particleEffect.StartParticle();

        AudioSystem.Instance.PlayPickingSound();
        Destroy(gameObject);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        
    }
}
