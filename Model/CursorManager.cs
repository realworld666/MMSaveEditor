// Decompiled with JetBrains decompiler
// Type: CursorManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorManager
{
  private Dictionary<int, Texture2D> mMouseTextures = new Dictionary<int, Texture2D>();
  private ComponentLookupCache<Selectable> selectableObjectLookupCache = new ComponentLookupCache<Selectable>(64);
  private ComponentLookupCache<CursorOptions> cursorOptionsLookupCache = new ComponentLookupCache<CursorOptions>(64);
  private const int cacheSize = 64;
  private CursorManager.State mState;
  private Selectable interactableObjectUnderCursor;

  public void Start()
  {
    for (CursorManager.State state = CursorManager.State.Normal; state < CursorManager.State.Count; ++state)
    {
      switch (state)
      {
        case CursorManager.State.Normal:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/Normal"));
          break;
        case CursorManager.State.Clickable:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/Link"));
          break;
        case CursorManager.State.TextField:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/Text"));
          break;
        case CursorManager.State.Draggable:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/OpenHand"));
          break;
        case CursorManager.State.Dragging:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/FistAdd"));
          break;
        case CursorManager.State.CanDrop:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/FistDrop"));
          break;
        case CursorManager.State.Unavailable:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/Unavailable"));
          break;
        case CursorManager.State.Tooltip:
          this.mMouseTextures.Add((int) state, UnityEngine.Resources.Load<Texture2D>("Art/Cursors/Help"));
          break;
      }
    }
  }

  private Vector2 GetOffset()
  {
    switch (this.mState)
    {
      case CursorManager.State.Normal:
        return new Vector2(9f, 9f);
      case CursorManager.State.Clickable:
        return new Vector2(11f, 9f);
      case CursorManager.State.TextField:
        return new Vector2(15f, 8f);
      case CursorManager.State.Draggable:
        return new Vector2(11f, 9f);
      case CursorManager.State.Dragging:
        return new Vector2(11f, 9f);
      case CursorManager.State.CanDrop:
        return new Vector2(11f, 9f);
      case CursorManager.State.Unavailable:
        return new Vector2(11f, 9f);
      case CursorManager.State.Tooltip:
        return new Vector2(11f, 9f);
      default:
        return new Vector2(0.0f, 0.0f);
    }
  }

  public void Update()
  {
    if (this.mState == CursorManager.State.Normal)
      this.CheckInteratibleState();
    Cursor.SetCursor(this.mMouseTextures[(int) this.mState], this.GetOffset(), CursorMode.Auto);
    Cursor.visible = true;
    this.mState = CursorManager.State.Normal;
  }

  private void CheckInteratibleState()
  {
    this.interactableObjectUnderCursor = this.FindInteractableObject(UIManager.instance.UIObjectsAtMousePosition);
    if ((Object) this.interactableObjectUnderCursor != (Object) null)
    {
      CursorOptions cursorOptions = this.cursorOptionsLookupCache.get(this.interactableObjectUnderCursor.gameObject);
      if ((Object) cursorOptions != (Object) null)
        this.mState = cursorOptions.state;
      else if (this.interactableObjectUnderCursor.IsInteractable())
        this.mState = CursorManager.State.Clickable;
      else
        this.mState = CursorManager.State.Unavailable;
    }
    else
      this.mState = CursorManager.State.Normal;
  }

  public Selectable FindInteractableObject(List<RaycastResult> inResults)
  {
    if (inResults.Count == 0)
      return (Selectable) null;
    for (Transform transform = inResults[0].gameObject.GetComponent<Transform>(); (Object) transform != (Object) null; transform = transform.parent)
    {
      Selectable selectable = this.selectableObjectLookupCache.get(transform.gameObject);
      if ((Object) selectable != (Object) null && selectable.IsInteractable())
        return selectable;
    }
    return (Selectable) null;
  }

  public void SetState(CursorManager.State inState)
  {
    this.mState = inState;
  }

  public Selectable GetInteractableObjectUnderCursor()
  {
    return this.interactableObjectUnderCursor;
  }

  public enum State
  {
    Normal,
    Clickable,
    TextField,
    Draggable,
    Dragging,
    CanDrop,
    Unavailable,
    Tooltip,
    Count,
  }
}
