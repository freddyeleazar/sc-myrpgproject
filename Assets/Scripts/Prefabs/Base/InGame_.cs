using Sirenix.OdinInspector;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;

public class InGame_ : Component_, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SpriteRenderer spriteRenderer;
    //public AnimatorController animatorController;
    public Animator animator;
    public string spriteBaseName;
    public List<AnimatedAction> animatedActions;
    public List<AnimatedActionAngle> animatedActionAngles;

    public Texture2D cursor;

    #region Animator Components
    //protected AnimatorStateMachine rootStateMachine;//Root State Machine
    #endregion

    public BottomUi bottomUi;
    public Player_ player;

    public override void Build(Owner_ owner)
    {
        base.Build(owner);
        bottomUi = FindObjectOfType<BottomUi>();
        player = FindObjectOfType<Player_>();
    }

    //[Button]
    //public void SetPixelsPerUnit(float pixelsPerUnit)
    //{
    //    string baseType = ((Base_)owner).baseType_.ToString();
    //    string spriteBaseName = ((InGame_)((Base_)owner).graphics_.inGame_).spriteBaseName;
    //    string spritesPath = $"Sprites/{baseType}s/{spriteBaseName}";
    //    List<Texture> textures = new List<Texture>(Resources.LoadAll<Texture>(spritesPath));
    //    foreach (Texture texture in textures)
    //    {
    //        string spritePath = AssetDatabase.GetAssetPath(texture);
    //        TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(spritePath);
    //        importer.textureType = TextureImporterType.Sprite;
    //        importer.spritePixelsPerUnit = pixelsPerUnit;
    //        AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);
    //    }
    //}

    //[Button]
    //public void SetPivot(float x, float y)
    //{
    //    string baseType = ((Base_)owner).baseType_.ToString();
    //    string spriteBsaeName = ((InGame_)((Base_)owner).graphics_.inGame_).spriteBaseName;
    //    string spritesFolderPath = $"Sprites/{baseType}s/{spriteBaseName}";
    //    List<Sprite> sprites = new List<Sprite>(Resources.LoadAll<Sprite>(spritesFolderPath));

    //    foreach (Sprite sprite in sprites)
    //    {
    //        string spritePath = AssetDatabase.GetAssetPath(sprite);
    //        TextureImporter textureImporter = (TextureImporter)TextureImporter.GetAtPath(spritePath);
    //        textureImporter.textureType = TextureImporterType.Sprite;

    //        TextureImporterSettings texSettings = new TextureImporterSettings();
    //        textureImporter.ReadTextureSettings(texSettings);
    //        texSettings.spriteAlignment = (int)SpriteAlignment.Custom;
    //        textureImporter.SetTextureSettings(texSettings);

    //        textureImporter.spritePivot = new Vector2(x, y);
    //        AssetDatabase.ImportAsset(spritePath, ImportAssetOptions.ForceUpdate);

    //        //EditorUtility.SetDirty(textureImporter);
    //        //textureImporter.SaveAndReimport();
    //    }
    //}

    [Button]
    public virtual void GenerateAnimationClips()
    {
        SetAnimator();
        foreach (AnimatedAction animatedAction in animatedActions)
        {
            foreach (AnimatedActionAngle animatedActionAngle in animatedActionAngles)
            {
                //GenerateAnimationClip(animatedAction, animatedActionAngle, out AnimationClip animationClip);
                //AddToAnimatorController(animationClip, animatedAction);
            }
        }
    }

    public virtual void AddToAnimatorController(AnimationClip animationClip, AnimatedAction animatedAction)
    {
    }

    //public void GenerateAnimationClip(AnimatedAction animatedAction, AnimatedActionAngle animatedActionAngle, out AnimationClip animationClip)
    //{
    //    animationClip = new AnimationClip();
    //    animationClip.name = $"{spriteBaseName}_{animatedAction}_{animatedActionAngle}.anim";

    //    EditorCurveBinding spriteBinding = new EditorCurveBinding();
    //    spriteBinding.type = typeof(SpriteRenderer);
    //    spriteBinding.path = "";
    //    spriteBinding.propertyName = "m_Sprite";

    //    string animationClipSpritesPath = $"Sprites/{((Base_)owner).baseType_}s/{spriteBaseName}/{animatedAction}/{animatedActionAngle}";
    //    Sprite[] animationClipSprites = Resources.LoadAll<Sprite>(animationClipSpritesPath);
    //    ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[animationClipSprites.Length + 1];
    //    for (int i = 0; i < animationClipSprites.Length; i++)
    //    {
    //        spriteKeyFrames[i] = new ObjectReferenceKeyframe();
    //        spriteKeyFrames[i].time = i;
    //        spriteKeyFrames[i].value = animationClipSprites[i];
    //        if(i == animationClipSprites.Length - 1)//Si es el último frame, repetirlo para que no se pase abruptamente al primero
    //        {
    //            spriteKeyFrames[i+1] = new ObjectReferenceKeyframe();
    //            spriteKeyFrames[i+1].time = i+1;
    //            spriteKeyFrames[i+1].value = animationClipSprites[i];
    //        }
    //    }

    //    AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(animationClip);
    //    settings.loopTime = true;
    //    AnimationUtility.SetAnimationClipSettings(animationClip, settings);

    //    AnimationUtility.SetObjectReferenceCurve(animationClip, spriteBinding, spriteKeyFrames);

    //    if (!AssetDatabase.IsValidFolder($"Assets/Animations/Animation Clips/{spriteBaseName}"))
    //        AssetDatabase.CreateFolder($"Assets/Animations/Animation Clips", $"{spriteBaseName}");
    //    string animationPath = $"Assets/Animations/Animation Clips/{spriteBaseName}/{animationClip.name}";
    //    AssetDatabase.CreateAsset(animationClip, animationPath);
    //    AssetDatabase.SaveAssets();
    //    AssetDatabase.Refresh();
    //}

    public virtual void SetAnimator()
    {
        //rootStateMachine = animatorController.layers[0].stateMachine;
    }

    [Button]
    public void DeleteAnimationClips()
    {
        EmptyAnimator();
        string animationPath = $"Assets/Animations/Animation Clips/{spriteBaseName}";
        //AssetDatabase.DeleteAsset(animationPath);
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();
    }

    [Button]
    public virtual void EmptyAnimator()
    {
        SetAnimator();
        //Implementar método que elimine las motions de: lista con los animatorStates y lista con los blends (en este último caso, de cada child)
        //Como no lo tengo aún, debo eliminar dichas motions manualmente desde el código de cada hijo referenciándolas directamente
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                bottomUi.LoadUI((Base_)owner);
                break;
            case PointerEventData.InputButton.Right:
                ((PlayerBehaviour_)player.mechanics_.behaviour_).DefaultAction(eventData, owner.gameObject, ((Base_)owner).objectType);
                break;
            case PointerEventData.InputButton.Middle:
                //Desplegar menú flotante con opciones de interacción
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

public enum AnimatedAction
{
    Idle,
    Walk,
    Run,
    Attack,
    UseBow,
    Die,
    Dropped,
    Stored
}

public enum AnimatedActionAngle
{
    Right,
    UpRight,
    Up,
    UpLeft,
    Left,
    DownLeft,
    Down,
    DownRight
}
