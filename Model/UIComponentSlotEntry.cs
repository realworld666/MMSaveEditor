// Decompiled with JetBrains decompiler
// Type: UIComponentSlotEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIComponentSlotEntry : MonoBehaviour
{
  public GameObject highLight;
  public TextMeshProUGUI[] slotName;
  public Image slotBacking;
  public Image backingImage;
  public UIPartComponentIcon icon;
  public GameObject fittedSlotContainer;
  public GameObject emptySlotContainer;
  public GameObject lockedSlotContainer;
  public Transform colorCodedPossibleComponentTypeParent;
  public Button removeComponentButton;
  private CarPartComponent mComponent;
  private int mLevel;
  private int mColorCount;

  private void Awake()
  {
    this.removeComponentButton.onClick.AddListener(new UnityAction(this.OnRemoveComponent));
  }

  private void OnEnable()
  {
    this.highLight.SetActive(false);
  }

  private void OnRemoveComponent()
  {
    if (this.mComponent == null)
      return;
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    CarPartDesign carPartDesign = Game.instance.player.team.carManager.carPartDesign;
    carPartDesign.RemoveComponent(carPartDesign.part, this.mComponent);
  }

  public void Setup(UIComponentSlotEntry.State inState, CarPartComponent inComponent, int inLevel)
  {
    this.mComponent = inComponent;
    this.removeComponentButton.gameObject.SetActive(false);
    this.mLevel = inLevel;
    this.mColorCount = this.mLevel + 1;
    for (int index = 0; index < this.slotName.Length; ++index)
      this.slotName[index].text = PartTypeSlotSettings.GetSlot(this.mLevel);
    this.backingImage.color = UIConstants.GetPartLevelColor(this.mColorCount);
    if (inComponent != null)
    {
      this.icon.Setup(inComponent);
      this.slotBacking.color = UIConstants.GetPartLevelColor(this.mComponent.level);
    }
    this.fittedSlotContainer.SetActive(inState == UIComponentSlotEntry.State.Used);
    this.emptySlotContainer.SetActive(inState == UIComponentSlotEntry.State.Empty);
    this.lockedSlotContainer.SetActive(inState == UIComponentSlotEntry.State.Locked);
    if (!((Object) this.colorCodedPossibleComponentTypeParent != (Object) null))
      return;
    for (int index = 0; index < this.colorCodedPossibleComponentTypeParent.childCount; ++index)
      this.colorCodedPossibleComponentTypeParent.GetChild(index).gameObject.SetActive(index < this.mColorCount);
  }

  public enum State
  {
    Locked,
    Empty,
    Used,
  }
}
