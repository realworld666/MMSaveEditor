// Decompiled with JetBrains decompiler
// Type: UIMethodEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMethodEntry : MonoBehaviour
{
  private string mMethodName = string.Empty;
  private ParameterInfo[] mParameters = new ParameterInfo[0];
  public TextMeshProUGUI name;
  public Toggle toggle;
  private DialogSystemTestBox mDialogSystemTestBox;

  private void Start()
  {
    this.mDialogSystemTestBox = UIManager.instance.dialogBoxManager.GetDialog<DialogSystemTestBox>();
  }

  public void Setup(string inName, ParameterInfo[] inParameters)
  {
    this.gameObject.SetActive(true);
    this.name.text = inName;
    this.mMethodName = inName;
    this.mParameters = inParameters;
    this.toggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnTogglePress));
  }

  private void OnTogglePress(bool inValue)
  {
    if (!inValue)
      return;
    this.mDialogSystemTestBox.selectedMethod = this.mMethodName;
    this.mDialogSystemTestBox.ClearParameters();
    if (this.mParameters.Length > 0)
      this.mDialogSystemTestBox.OpenParametersBox(this.mParameters);
    else
      this.mDialogSystemTestBox.parametersObject.SetActive(false);
  }
}
