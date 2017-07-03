// Decompiled with JetBrains decompiler
// Type: GenericConfirmationCharacterDialog
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;

public class GenericConfirmationCharacterDialog : UIDialogBox
{
  public UICharacterPortrait portrait;
  public Flag flag;
  public TextMeshProUGUI nameLabel;
  public TextMeshProUGUI roleLabel;
  public TextMeshProUGUI titleLabel;
  public TextMeshProUGUI descriptionLabel;

  protected override void Awake()
  {
    base.Awake();
  }

  protected override void OnEnable()
  {
    base.OnEnable();
  }

  public static void Show(Person inPerson, string inTitle, string inDescription, Action inYesAction, Action inNoAction)
  {
    GenericConfirmationCharacterDialog dialog = UIManager.instance.dialogBoxManager.GetDialog<GenericConfirmationCharacterDialog>();
    dialog.portrait.SetPortrait(inPerson);
    dialog.flag.SetNationality(inPerson.nationality);
    dialog.nameLabel.text = inPerson.shortName;
    dialog.roleLabel.text = Localisation.LocaliseEnum((Enum) inPerson.contract.job);
    dialog.titleLabel.text = inTitle;
    dialog.descriptionLabel.text = inDescription;
    dialog.OnYesButton += inYesAction;
    dialog.OnNoButton += inNoAction;
    UIManager.instance.dialogBoxManager.Show("GenericConfirmationCharacter");
  }
}
