// Decompiled with JetBrains decompiler
// Type: ColorPickerDialogBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class ColorPickerDialogBox : UIDialogBox, IEventSystemHandler, IDragHandler, IBeginDragHandler
{
  private Color mCurrentColor = Color.white;
  private Vector3 mOffset = Vector3.zero;
  private Vector3 mPosition = Vector3.zero;
  private Vector3 mScreenEdgeBottomLeft = Vector3.zero;
  private Vector3 mScreenEdgeTopRight = Vector3.zero;
  private Vector3[] mCorners = new Vector3[4];
  private Vector2 mAnchor = new Vector2(0.5f, 0.5f);
  public static Action<Color> OnColorPicked;
  public static Action<Color> OnColorPickConfirm;
  public static Action OnClose;
  public RectTransform rectTransform;
  public HSVPicker colorPicker;
  public Button confirmColorButton;
  public Button cancelColorButton;
  public Button closeButton;
  public UIGridList customColorsGrid;
  public Button AddCustomColorButton;
  public Button RemoveCustomColorButton;
  public ToggleGroup customColorsToggleGroup;
  public GameObject editCustomColorButtons;
  public Canvas canvas;
  private RectTransform mRectTransform;
  private Color[] mDefaultColors;
  private bool mRestrictedMode;
  private float mWidth;
  private float mHeight;

  public static void Open(RectTransform inRect, Color inCurrentColor, Color[] inDefaultColors = null, bool inRestrictedMode = false)
  {
    ColorPickerDialogBox dialog = UIManager.instance.dialogBoxManager.GetDialog<ColorPickerDialogBox>();
    if (dialog.gameObject.activeSelf)
      dialog.Hide();
    dialog.Show(inRect, inCurrentColor, inDefaultColors, inRestrictedMode);
  }

  public static void Close()
  {
    ColorPickerDialogBox dialog = UIManager.instance.dialogBoxManager.GetDialog<ColorPickerDialogBox>();
    if (!dialog.gameObject.activeSelf)
      return;
    dialog.Hide();
  }

  protected override void Awake()
  {
    base.Awake();
    this.confirmColorButton.onClick.AddListener(new UnityAction(this.OnColorConfirmButton));
    this.cancelColorButton.onClick.AddListener(new UnityAction(this.OnColorCancelButton));
    this.closeButton.onClick.AddListener(new UnityAction(this.OnColorCancelButton));
    this.AddCustomColorButton.onClick.AddListener(new UnityAction(this.OnCustomColorAddButton));
    this.RemoveCustomColorButton.onClick.AddListener(new UnityAction(this.OnCustomColorRemoveButton));
  }

  public void Show(RectTransform inRect, Color inCurrentColor, Color[] inDefaultColors = null, bool inRestrictedMode = false)
  {
    this.mRectTransform = inRect;
    this.mCurrentColor = inCurrentColor;
    this.mDefaultColors = inDefaultColors;
    this.mRestrictedMode = inRestrictedMode;
    GameUtility.SetActive(this.colorPicker.gameObject, !this.mRestrictedMode);
    GameUtility.SetActive(this.editCustomColorButtons, !this.mRestrictedMode);
    this.colorPicker.onValueChanged.RemoveAllListeners();
    this.SetCustomColorsGrid(!this.mRestrictedMode ? ColorPreferences.customColors : this.mDefaultColors);
    GameUtility.SetActive(this.gameObject, true);
    GameUtility.SetTooltipTransformInsideScreen(this.rectTransform, this.mRectTransform, new Vector3(), false, (RectTransform) null);
    this.colorPicker.AssignColor(inCurrentColor, true);
    this.colorPicker.onValueChanged.AddListener(new UnityAction<Color>(this.OnColorChange));
    this.UpdateCustomColorButtonsState(true);
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  private void SetPivot()
  {
    Vector2 vector2 = this.rectTransform.pivot - this.mAnchor;
    Vector3 vector3_1 = new Vector3(vector2.x * this.rectTransform.rect.size.x, vector2.y * this.rectTransform.rect.size.y);
    this.rectTransform.pivot = this.mAnchor;
    RectTransform rectTransform = this.rectTransform;
    Vector3 vector3_2 = rectTransform.localPosition - vector3_1;
    rectTransform.localPosition = vector3_2;
  }

  public override void Hide()
  {
    if (ColorPickerDialogBox.OnClose != null)
      ColorPickerDialogBox.OnClose();
    this.colorPicker.onValueChanged.RemoveAllListeners();
    ColorPickerDialogBox.OnColorPicked = (Action<Color>) null;
    ColorPickerDialogBox.OnColorPickConfirm = (Action<Color>) null;
    ColorPickerDialogBox.OnClose = (Action) null;
    ColorPreferences.SaveCustomColors();
    base.Hide();
  }

  private void OnColorChange(Color inColor)
  {
    if (ColorPickerDialogBox.OnColorPicked != null)
      ColorPickerDialogBox.OnColorPicked(inColor);
    this.UpdateCustomColorButtonsState(!this.mRestrictedMode);
  }

  private void OnColorConfirmButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (ColorPickerDialogBox.OnColorPickConfirm != null)
      ColorPickerDialogBox.OnColorPickConfirm(this.colorPicker.currentColor);
    this.Hide();
  }

  private void OnColorCancelButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.colorPicker.AssignColor(this.mCurrentColor, true);
    this.Hide();
  }

  private void SetCustomColorsGrid(Color[] inColors)
  {
    this.customColorsToggleGroup.SetAllTogglesOff();
    this.customColorsGrid.DestroyListItems();
    for (int index = 0; index < inColors.Length; ++index)
      this.customColorsGrid.CreateListItem<ColorPickerCustomColorEntry>().Setup(inColors[index], this);
  }

  public void SelectCustomColor(Color inColor)
  {
    this.colorPicker.AssignColor(inColor, true);
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
  }

  private void OnCustomColorAddButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (ColorPreferences.CanAddCustomColor(this.colorPicker.currentColor))
      ColorPreferences.AddCustomColor(this.colorPicker.currentColor);
    this.SetCustomColorsGrid(ColorPreferences.customColors);
    this.UpdateCustomColorButtonsState(true);
  }

  private void OnCustomColorRemoveButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    if (ColorPreferences.CanRemoveCustomColor(this.colorPicker.currentColor))
      ColorPreferences.RemoveCustomColor(this.colorPicker.currentColor);
    this.SetCustomColorsGrid(ColorPreferences.customColors);
    this.UpdateCustomColorButtonsState(true);
  }

  private void UpdateCustomColorButtonsState(bool inAutoActivateToggles = false)
  {
    this.AddCustomColorButton.interactable = ColorPreferences.CanAddCustomColor(this.colorPicker.currentColor);
    this.RemoveCustomColorButton.interactable = ColorPreferences.CanRemoveCustomColor(this.colorPicker.currentColor);
    if (!inAutoActivateToggles)
      return;
    this.customColorsToggleGroup.SetAllTogglesOff();
    int itemCount = this.customColorsGrid.itemCount;
    for (int inIndex = 0; inIndex < itemCount; ++inIndex)
    {
      ColorPickerCustomColorEntry customColorEntry = this.customColorsGrid.GetItem<ColorPickerCustomColorEntry>(inIndex);
      if (GameUtility.ColorEquals(customColorEntry.color, this.colorPicker.currentColor))
      {
        customColorEntry.SetToggle(true);
        break;
      }
    }
  }

  public void OnBeginDrag(PointerEventData inPointerData)
  {
    if (!((UnityEngine.Object) this.canvas != (UnityEngine.Object) null) || this.canvas.renderMode != UnityEngine.RenderMode.ScreenSpaceCamera)
      return;
    this.SetPivot();
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, inPointerData.position, this.canvas.worldCamera, out localPoint);
    this.mOffset = this.transform.position - this.canvas.transform.TransformPoint((Vector3) localPoint);
    this.mScreenEdgeBottomLeft = UIManager.instance.UICamera.ScreenToWorldPoint(Vector3.zero);
    this.mScreenEdgeTopRight.Set(Mathf.Abs(this.mScreenEdgeBottomLeft.x), Mathf.Abs(this.mScreenEdgeBottomLeft.y), 0.0f);
    this.rectTransform.GetWorldCorners(this.mCorners);
    this.mWidth = Mathf.Abs(this.mCorners[1].x - this.mCorners[2].x);
    this.mHeight = Mathf.Abs(this.mCorners[1].y - this.mCorners[3].y);
  }

  public void OnDrag(PointerEventData inPointerData)
  {
    if ((UnityEngine.Object) this.canvas == (UnityEngine.Object) null || this.canvas.renderMode != UnityEngine.RenderMode.ScreenSpaceCamera)
    {
      this.transform.position += (Vector3) inPointerData.delta;
    }
    else
    {
      Vector2 localPoint;
      RectTransformUtility.ScreenPointToLocalPointInRectangle(this.canvas.transform as RectTransform, inPointerData.position, this.canvas.worldCamera, out localPoint);
      this.mPosition = this.canvas.transform.TransformPoint((Vector3) localPoint) + this.mOffset;
      this.mPosition.x = Mathf.Clamp(this.mPosition.x, this.mScreenEdgeBottomLeft.x + this.mWidth / 2f, this.mScreenEdgeTopRight.x - this.mWidth / 2f);
      this.mPosition.y = Mathf.Clamp(this.mPosition.y, this.mScreenEdgeBottomLeft.y + this.mHeight / 2f, this.mScreenEdgeTopRight.y - this.mHeight / 2f);
      this.transform.position = this.mPosition;
    }
  }
}
