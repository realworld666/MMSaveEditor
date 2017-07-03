// Decompiled with JetBrains decompiler
// Type: UITeamInvestorOffers
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine.Events;
using UnityEngine.UI;

public class UITeamInvestorOffers : UIDialogBox
{
  public UIGridList grid;
  public Button closeButton;

  protected override void Awake()
  {
    base.Awake();
    this.closeButton.onClick.AddListener(new UnityAction(this.OnCloseButton));
  }

  public static void Open()
  {
    UITeamInvestorOffers dialog = UIManager.instance.dialogBoxManager.GetDialog<UITeamInvestorOffers>();
    dialog.Setup();
    UIManager.instance.dialogBoxManager.Show((UIDialogBox) dialog);
  }

  public static void Close()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UITeamInvestorOffers>().Hide();
  }

  public void Setup()
  {
    this.grid.DestroyListItems();
    int count = Game.instance.investorManager.count;
    for (int inIndex = 0; inIndex < count; ++inIndex)
    {
      UITeamInvestorOfferEntry listItem = this.grid.CreateListItem<UITeamInvestorOfferEntry>();
      listItem.Setup(Game.instance.investorManager.GetEntity(inIndex));
      GameUtility.SetActive(listItem.gameObject, true);
    }
    scSoundManager.Instance.PlaySound(SoundID.Sfx_TransitionScreen, 0.0f);
  }

  public void SelectInvestor(Investor inInvestor)
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
    this.Hide();
    UIManager.instance.GetScreen<CreateTeamScreen>().StartGameInCustomTeam(inInvestor);
  }

  private void OnCloseButton()
  {
    scSoundManager.Instance.PlaySound(SoundID.Button_Back, 0.0f);
    this.Hide();
  }
}
