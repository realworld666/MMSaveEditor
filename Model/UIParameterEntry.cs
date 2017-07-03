// Decompiled with JetBrains decompiler
// Type: UIParameterEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIParameterEntry : MonoBehaviour
{
  public TextMeshProUGUI name;
  public Toggle toggle;
  private object mObject;
  private DialogSystemTestBox mDialogSystemTestBox;

  public void Setup(string inName, object inObject)
  {
    this.mDialogSystemTestBox = UIManager.instance.dialogBoxManager.GetDialog<DialogSystemTestBox>();
    this.gameObject.SetActive(true);
    this.name.text = inName;
    this.mObject = inObject;
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnTogglePress));
  }

  private void OnTogglePress(bool inValue)
  {
    if (!inValue)
      return;
    this.mDialogSystemTestBox.SetParameterObject(this.mObject);
  }
}
