// Decompiled with JetBrains decompiler
// Type: UIGraphLineConnector
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

public class UIGraphLineConnector : MonoBehaviour
{
  public Transform startPoint;
  public UIGraphLineConnector.CalculationState calculationState;
  public UIGraphLineConnector.Animate animate;
  public Transform endPoint;
  public float lineWidth;
  public Sprite regularLine;
  public Sprite dottedLine;
  private float mTargetWidth;
  private float mTimer;

  private void Start()
  {
    this.SetupLine();
  }

  private void Update()
  {
    switch (this.animate)
    {
      case UIGraphLineConnector.Animate.DontAnimate:
        this.SetupLine();
        break;
      case UIGraphLineConnector.Animate.GrowTowards:
        this.GrowTowardsAnim();
        break;
    }
  }

  private void GrowTowardsAnim()
  {
    this.mTimer += GameTimer.deltaTime;
    RectTransform component = this.gameObject.GetComponent<RectTransform>();
    Rect rect = component.rect;
    rect.width = Mathf.Lerp(0.0f, this.mTargetWidth, this.mTimer);
    rect.height = this.lineWidth;
    Color color = this.gameObject.GetComponent<Image>().color;
    color.a = Mathf.Sin(this.mTimer * 3.141593f);
    this.gameObject.GetComponent<Image>().color = color;
    component.sizeDelta = rect.size;
    if ((double) this.mTimer < 1.0)
      return;
    this.SetState(UIGraphLineConnector.Animate.DontAnimate);
    this.mTimer = 0.0f;
  }

  private void SetState(UIGraphLineConnector.Animate inState)
  {
    this.animate = inState;
  }

  private void SetupLine()
  {
    RectTransform component1 = this.startPoint.GetComponent<RectTransform>();
    RectTransform component2 = this.endPoint.GetComponent<RectTransform>();
    RectTransform component3 = this.gameObject.GetComponent<RectTransform>();
    Vector3 vector3 = this.endPoint.position - this.startPoint.position;
    component3.position = component1.position;
    Rect rect = component3.rect;
    float num1 = Vector2.Distance((Vector2) component1.anchoredPosition3D, (Vector2) component2.anchoredPosition3D);
    if ((double) num1 == 0.0 || this.calculationState == UIGraphLineConnector.CalculationState.IgnoreHipotnuse)
      num1 = 1f / this.gameObject.GetComponent<Image>().canvas.scaleFactor * Vector2.Distance((Vector2) component1.position, (Vector2) component2.position);
    rect.width = num1;
    rect.height = this.lineWidth;
    component3.sizeDelta = rect.size;
    float num2 = Vector3.Angle(Vector3.right, vector3);
    if ((double) Vector3.Cross(Vector3.right, vector3).z < 0.0)
      num2 = -num2;
    Vector3 eulerAngles = component3.eulerAngles;
    eulerAngles.z = num2;
    component3.eulerAngles = eulerAngles;
    this.mTargetWidth = num1;
  }

  public void SetToDottedLine()
  {
    this.gameObject.GetComponent<Image>().sprite = this.dottedLine;
  }

  public void SetToRegularLine()
  {
    this.gameObject.GetComponent<Image>().sprite = this.regularLine;
  }

  public void SetColor(Color inColor)
  {
    this.gameObject.GetComponent<Image>().color = inColor;
  }

  public Color GetColor()
  {
    return this.gameObject.GetComponent<Image>().color;
  }

  public enum Animate
  {
    DontAnimate,
    GrowTowards,
  }

  public enum CalculationState
  {
    Regular,
    IgnoreHipotnuse,
  }
}
