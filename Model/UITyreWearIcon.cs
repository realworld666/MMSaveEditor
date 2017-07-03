// Decompiled with JetBrains decompiler
// Type: UITyreWearIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITyreWearIcon : MonoBehaviour
{
  private TyreSet.Compound mTyreSetCompound = TyreSet.Compound.Soft;
  private List<SessionCarBonuses.DisplayBonusInfo> mBonuses = new List<SessionCarBonuses.DisplayBonusInfo>();
  private float mFillAmount = -1f;
  [SerializeField]
  private UITyreWearIcon.IconSize iconSize;
  [SerializeField]
  private Image tyreWearFillImage;
  [SerializeField]
  private Image tyreIcon;
  public Toggle toggle;
  public GameObject currentTyreIcon;
  public GameObject newSelectionTyreIcon;
  public GameObject lockedTyreIcon;
  public GameObject unlockTyreIcon;
  private TyreSet mTyreSet;
  private bool mIsLocked;
  private bool mIsNewlyUnlocked;
  private EventTrigger rolloverTrigger;

  public TyreSet.Compound compound
  {
    get
    {
      return this.mTyreSetCompound;
    }
  }

  public TyreSet tyreSet
  {
    get
    {
      return this.mTyreSet;
    }
  }

  private void Awake()
  {
    this.UpdateRolloverTriggers();
    this.tyreWearFillImage.fillClockwise = false;
  }

  private void Update()
  {
    if (this.mTyreSet == null || !((Object) this.tyreWearFillImage != (Object) null) || (double) this.mFillAmount == (double) this.mTyreSet.GetCondition())
      return;
    this.mFillAmount = this.mTyreSet.GetCondition();
    GameUtility.SetImageFillAmountIfDifferent(this.tyreWearFillImage, this.mFillAmount, 1f / 512f);
  }

  private void OnEnable()
  {
    this.UpdateIcon();
    this.UpdateColor();
  }

  public void SetTyreSet(TyreSet inTyreSet, List<SessionCarBonuses.DisplayBonusInfo> inBonuses = null)
  {
    if (inTyreSet == null)
      return;
    this.mTyreSet = inTyreSet;
    this.mTyreSetCompound = inTyreSet.GetCompound();
    this.mBonuses = inBonuses;
    this.UpdateIcon();
    this.UpdateColor();
    GameUtility.SetImageFillAmountIfDifferent(this.tyreWearFillImage, this.mTyreSet.GetCondition(), 1f / 512f);
    this.transform.parent.gameObject.SetActive(true);
  }

  private void UpdateIcon()
  {
    if (this.mTyreSet == null)
      return;
    string str = this.iconSize != UITyreWearIcon.IconSize.Small ? "TyresNew-Large-Tyre" : "TyresNew-Mini-MiniTyre";
    switch (this.mTyreSetCompound)
    {
      case TyreSet.Compound.SuperSoft:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Soft");
        break;
      case TyreSet.Compound.Soft:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "SuperSoft");
        break;
      case TyreSet.Compound.Medium:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Medium");
        break;
      case TyreSet.Compound.Hard:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Hard");
        break;
      case TyreSet.Compound.Intermediate:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Inter");
        break;
      case TyreSet.Compound.Wet:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, str + "Wet");
        break;
      case TyreSet.Compound.UltraSoft:
        this.tyreIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Shared1, this.iconSize != UITyreWearIcon.IconSize.Small ? "TyresNew-Large-UltraSoft" : "TyresNew-Small-UltraSoft");
        break;
    }
  }

  private void UpdateColor()
  {
    if (this.mTyreSet == null)
      return;
    this.tyreWearFillImage.color = this.mTyreSet.GetColor();
  }

  private void OnButtonPressed()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    PitScreen screen = UIManager.instance.GetScreen<PitScreen>();
    if (!((Object) screen != (Object) null) || !screen.hasFocus || !this.toggle.interactable)
      return;
    screen.optionsSelectionWidget.tyreSelectionWidget.SelectTyre(this.mTyreSet);
  }

  private void OnMouseRolloverEnter()
  {
    if (!this.DisplayRollover())
      return;
    UIManager.instance.dialogBoxManager.GetDialog<TyreInfoRollover>().Show(this.mIsLocked, this.mTyreSet, Game.instance.sessionManager.eventDetails.circuit, this.mBonuses, this.mIsNewlyUnlocked);
  }

  private void OnMouseRolloverExit()
  {
    if (!this.DisplayRollover())
      return;
    UIManager.instance.dialogBoxManager.GetDialog<TyreInfoRollover>().Hide();
  }

  private bool DisplayRollover()
  {
    UIScreen currentScreen = UIManager.instance.currentScreen;
    if (currentScreen is PitScreen)
      return true;
    if (!(currentScreen is GridScreen))
      return false;
    if (!this.mIsLocked)
      return this.mIsNewlyUnlocked;
    return true;
  }

  public void UpdateTyreLocking(RacingVehicle inVehicle, bool inAllowLocking = true)
  {
    this.UpdateTyreLocked(inVehicle, inAllowLocking);
    this.UpdateTyreUnlockedForSafety(inVehicle, inAllowLocking);
  }

  private void UpdateTyreLocked(RacingVehicle inVehicle, bool inAllowLocking)
  {
    this.mIsLocked = inVehicle.strategy.lockedTyres == this.mTyreSet && inAllowLocking;
    this.UpdateRolloverTriggers();
    if ((Object) this.lockedTyreIcon != (Object) null)
      GameUtility.SetActive(this.lockedTyreIcon, this.mIsLocked);
    if (!((Object) this.toggle != (Object) null))
      return;
    this.toggle.interactable = !this.mIsLocked;
  }

  private void UpdateTyreUnlockedForSafety(RacingVehicle inVehicle, bool inAllowLocking)
  {
    this.mIsNewlyUnlocked = !this.mIsLocked && Game.instance.sessionManager.eventDetails.results.GetSessionIdDriverElimination(inVehicle.driver) == 2 && inAllowLocking;
    this.UpdateRolloverTriggers();
    if (!((Object) this.unlockTyreIcon != (Object) null))
      return;
    GameUtility.SetActive(this.unlockTyreIcon, this.mIsNewlyUnlocked);
  }

  private void UpdateRolloverTriggers()
  {
    if ((Object) this.toggle != (Object) null || this.mIsLocked || this.mIsNewlyUnlocked)
    {
      if ((Object) this.toggle == (Object) null)
      {
        if ((Object) this.gameObject.GetComponent<EventTrigger>() == (Object) null)
        {
          this.rolloverTrigger = this.gameObject.AddComponent<EventTrigger>();
        }
        else
        {
          this.rolloverTrigger = this.gameObject.GetComponent<EventTrigger>();
          this.rolloverTrigger.get_triggers().Clear();
        }
      }
      else
      {
        if ((Object) this.toggle.gameObject.GetComponent<EventTrigger>() == (Object) null)
        {
          this.rolloverTrigger = this.toggle.gameObject.AddComponent<EventTrigger>();
        }
        else
        {
          this.rolloverTrigger = this.toggle.gameObject.GetComponent<EventTrigger>();
          this.rolloverTrigger.get_triggers().Clear();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = UnityEngine.EventSystems.EventTriggerType.PointerClick;
        entry.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnButtonPressed()));
        this.rolloverTrigger.get_triggers().Add(entry);
      }
      EventTrigger.Entry entry1 = new EventTrigger.Entry();
      entry1.eventID = UnityEngine.EventSystems.EventTriggerType.PointerEnter;
      entry1.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseRolloverEnter()));
      this.rolloverTrigger.get_triggers().Add(entry1);
      EventTrigger.Entry entry2 = new EventTrigger.Entry();
      entry2.eventID = UnityEngine.EventSystems.EventTriggerType.PointerExit;
      entry2.callback.AddListener((UnityAction<BaseEventData>) (eventData => this.OnMouseRolloverExit()));
      this.rolloverTrigger.get_triggers().Add(entry2);
    }
    else
    {
      if (!((Object) this.rolloverTrigger != (Object) null))
        return;
      this.rolloverTrigger.get_triggers().Clear();
    }
  }

  public enum IconSize
  {
    Small,
    Large,
  }
}
