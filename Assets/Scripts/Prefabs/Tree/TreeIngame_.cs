using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreeIngame_ : InGame_, IPointerClickHandler
{
    public List<Animation2D> animations;

    public override void Build(Owner_ owner)
    {
        base.Build(owner);
        GenerateAnimations();
        SetRandomSprite();
    }

    [Button]
    public void GenerateAnimations()
    {
        animations = GetComponents<Animation2D>().ToList();
        foreach (Animation2D animation in animations)
        {
            List<Sprite> animationsSprites = Resources.LoadAll<Sprite>($"Sprites/Trees/{((Tree_)owner).treeName}/{animation.animationName}").ToList();
            if (animation.animationName == "Idle" || animation.animationName == "FallingDown")
                animation.frames = animationsSprites.OrderBy(t => short.Parse(t.name)).ToArray();
            else
                animation.frames = animationsSprites.ToArray();
        }
    }

    [Button]
    public void EmptyAnimations()
    {
        animations.ForEach(t => Array.Resize(ref t.frames, 0));
        spriteRenderer.sprite = null;
    }

    public void SetRandomSprite()
    {
        List<Sprite> idleSprites = animations.Find(t => t.animationName == "Idle").frames.ToList();
        spriteRenderer.sprite = idleSprites[UnityEngine.Random.Range(0, idleSprites.Count())];
    }
}
