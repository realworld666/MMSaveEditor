using MMSaveEditor.Utils;
using System;
using UnityEngine;

public static class EasingUtility
{
    public static float EaseByType(EasingUtility.Easing e, float start, float end, float t)
    {
        t = (t).Clamp(0f, 1f);
        switch (e)
        {
            case EasingUtility.Easing.Linear:
                return EasingUtility.Linear(start, end, t);
            case EasingUtility.Easing.InQuad:
                return EasingUtility.InQuad(start, end, t);
            case EasingUtility.Easing.OutQuad:
                return EasingUtility.OutQuad(start, end, t);
            case EasingUtility.Easing.InOutQuad:
                return EasingUtility.InOutQuad(start, end, t);
            case EasingUtility.Easing.OutInQuad:
                return EasingUtility.OutInQuad(start, end, t);
            case EasingUtility.Easing.InCubic:
                return EasingUtility.InCubic(start, end, t);
            case EasingUtility.Easing.OutCubic:
                return EasingUtility.OutCubic(start, end, t);
            case EasingUtility.Easing.InOutCubic:
                return EasingUtility.InOutCubic(start, end, t);
            case EasingUtility.Easing.OutInCubic:
                return EasingUtility.OutInCubic(start, end, t);
            case EasingUtility.Easing.InQuart:
                return EasingUtility.InQuart(start, end, t);
            case EasingUtility.Easing.OutQuart:
                return EasingUtility.OutQuart(start, end, t);
            case EasingUtility.Easing.InOutQuart:
                return EasingUtility.InOutQuart(start, end, t);
            case EasingUtility.Easing.OutInQuart:
                return EasingUtility.OutInQuart(start, end, t);
            case EasingUtility.Easing.InQuint:
                return EasingUtility.InQuint(start, end, t);
            case EasingUtility.Easing.OutQuint:
                return EasingUtility.OutQuint(start, end, t);
            case EasingUtility.Easing.InOutQuint:
                return EasingUtility.InOutQuint(start, end, t);
            case EasingUtility.Easing.OutInQuint:
                return EasingUtility.OutInQuint(start, end, t);
            case EasingUtility.Easing.InSin:
                return EasingUtility.InSin(start, end, t);
            case EasingUtility.Easing.OutSin:
                return EasingUtility.OutSin(start, end, t);
            case EasingUtility.Easing.InOutSin:
                return EasingUtility.InOutSin(start, end, t);
            case EasingUtility.Easing.OutInSin:
                return EasingUtility.OutInSin(start, end, t);
            case EasingUtility.Easing.InExp:
                return EasingUtility.InExp(start, end, t);
            case EasingUtility.Easing.OutExp:
                return EasingUtility.OutExp(start, end, t);
            case EasingUtility.Easing.InOutExp:
                return EasingUtility.InOutExp(start, end, t);
            case EasingUtility.Easing.OutInExp:
                return EasingUtility.OutInExp(start, end, t);
            case EasingUtility.Easing.InCirc:
                return EasingUtility.InCirc(start, end, t);
            case EasingUtility.Easing.OutCirc:
                return EasingUtility.OutCirc(start, end, t);
            case EasingUtility.Easing.InOutCirc:
                return EasingUtility.InOutCirc(start, end, t);
            case EasingUtility.Easing.OutInCirc:
                return EasingUtility.OutInCirc(start, end, t);
            case EasingUtility.Easing.InElastic:
                return EasingUtility.InElastic(start, end, t);
            case EasingUtility.Easing.OutElastic:
                return EasingUtility.OutElastic(start, end, t);
            case EasingUtility.Easing.InOutElastic:
                return EasingUtility.InOutElastic(start, end, t);
            case EasingUtility.Easing.OutInElastic:
                return EasingUtility.OutInElastic(start, end, t);
            case EasingUtility.Easing.InBounce:
                return EasingUtility.InBounce(start, end, t);
            case EasingUtility.Easing.OutBounce:
                return EasingUtility.OutBounce(start, end, t);
            case EasingUtility.Easing.InOutBounce:
                return EasingUtility.InOutBounce(start, end, t);
            case EasingUtility.Easing.OutInBounce:
                return EasingUtility.OutInBounce(start, end, t);
            case EasingUtility.Easing.InBack:
                return EasingUtility.InBack(start, end, t);
            case EasingUtility.Easing.OutBack:
                return EasingUtility.OutBack(start, end, t);
            case EasingUtility.Easing.InOutBack:
                return EasingUtility.InOutBack(start, end, t);
            case EasingUtility.Easing.OutInBack:
                return EasingUtility.OutInBack(start, end, t);
            default:
                return 0.0f;
        }
    }

    public static float Linear(float start, float end, float t)
    {
        return t * (end - start) + start;
    }

    public static float InQuad(float start, float end, float t)
    {
        return (float)((double)t * (double)t * ((double)end - (double)start)) + start;
    }

    public static float OutQuad(float start, float end, float t)
    {
        return (float)(-((double)t * ((double)t - 2.0)) * ((double)end - (double)start)) + start;
    }

    public static float InOutQuad(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * ((double)end - (double)start)) + start;
        --t;
        return (float)(-0.5 * ((double)t * ((double)t - 2.0) - 1.0) * ((double)end - (double)start)) + start;
    }

    public static float OutInQuad(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)((-0.5 * ((double)t * ((double)t - 2.0) - 1.0) - 0.5) * ((double)end - (double)start)) + start;
        --t;
        return (float)((0.5 * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InCubic(float start, float end, float t)
    {
        return (float)((double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
    }

    public static float OutCubic(float start, float end, float t)
    {
        --t;
        return (float)(((double)t * (double)t * (double)t + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float InOutCubic(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
        t -= 2f;
        return (float)(0.5 * ((double)t * (double)t * (double)t + 2.0) * ((double)end - (double)start)) + start;
    }

    public static float OutInCubic(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)((0.5 * ((double)t * (double)t * (double)t + 2.0) - 0.5) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)((0.5 * (double)t * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InQuart(float start, float end, float t)
    {
        return (float)((double)t * (double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
    }

    public static float OutQuart(float start, float end, float t)
    {
        --t;
        return (float)(-((double)t * (double)t * (double)t * (double)t - 1.0) * ((double)end - (double)start)) + start;
    }

    public static float InOutQuart(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
        t -= 2f;
        return (float)(-0.5 * ((double)t * (double)t * (double)t * (double)t - 2.0) * ((double)end - (double)start)) + start;
    }

    public static float OutInQuart(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)((-0.5 * (double)t * (double)t * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)((0.5 * (double)t * (double)t * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InQuint(float start, float end, float t)
    {
        return (float)((double)t * (double)t * (double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
    }

    public static float OutQuint(float start, float end, float t)
    {
        --t;
        return (float)(((double)t * (double)t * (double)t * (double)t * (double)t + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float InOutQuint(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * (double)t * (double)t * (double)t * ((double)end - (double)start)) + start;
        t -= 2f;
        return (float)((0.5 * (double)t * (double)t * (double)t * (double)t * (double)t + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float OutInQuint(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)((0.5 * ((double)t * (double)t * (double)t * (double)t * (double)t) + 0.5) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)((0.5 * (double)t * (double)t * (double)t * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InSin(float start, float end, float t)
    {
        return (float)(-(double)Math.Cos((float)((double)t * 3.14159274101257 * 0.5)) * ((double)end - (double)start)) + end;
    }

    public static float OutSin(float start, float end, float t)
    {
        return (float)Math.Sin((float)((double)t * 3.14159274101257 * 0.5)) * (end - start) + start;
    }

    public static float InOutSin(float start, float end, float t)
    {
        return (float)((-0.5 * (double)Math.Cos(t * 3.141593f) + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float OutInSin(float start, float end, float t)
    {
        return (float)(((double)t - (-0.5 * (double)Math.Cos(t * 3.141593f) + 0.5 - (double)t)) * ((double)end - (double)start)) + start;
    }

    public static float InExp(float start, float end, float t)
    {
        return (float)Math.Pow(2f, (float)(10.0 * ((double)t - 1.0))) * (end - start) + start;
    }

    public static float OutExp(float start, float end, float t)
    {
        return (float)((1.0 - (double)Math.Pow(2f, -10f * t)) * ((double)end - (double)start)) + start;
    }

    public static float InOutExp(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)Math.Pow(2f, (float)(10.0 * ((double)t - 1.0))) * ((double)end - (double)start)) + start;
        --t;
        return (float)(0.5 * (2.0 - (double)Math.Pow(2f, -10f * t)) * ((double)end - (double)start)) + start;
    }

    public static float OutInExp(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)((0.5 * (2.0 - (double)Math.Pow(2f, -10f * t)) - 0.5) * ((double)end - (double)start)) + start;
        --t;
        return (float)((0.5 * (double)Math.Pow(2f, (float)(10.0 * ((double)t - 1.0))) + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InCirc(float start, float end, float t)
    {
        return (float)(-((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) - 1.0) * ((double)end - (double)start)) + start;
    }

    public static float OutCirc(float start, float end, float t)
    {
        --t;
        return (float)Math.Sqrt((float)(1.0 - (double)t * (double)t)) * (end - start) + start;
    }

    public static float InOutCirc(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(-0.5 * ((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) - 1.0) * ((double)end - (double)start)) + start;
        t -= 2f;
        return (float)(0.5 * ((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float OutInCirc(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(0.5 * (double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)((-0.5 * (double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float InElastic(float start, float end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        --t;
        return (float)(-(double)Math.Pow(2f, 10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * ((double)end - (double)start)) + start;
    }

    public static float OutElastic(float start, float end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        return (float)((double)Math.Pow(2f, -10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * ((double)end - (double)start)) + end;
    }

    public static float InOutElastic(float start, float end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(-0.5 * (double)Math.Pow(2f, 10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)(0.5 * (double)Math.Pow(2f, -10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * ((double)end - (double)start)) + end;
    }

    public static float OutInElastic(float start, float end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        t *= 2f;
        if ((double)t < 1.0)
            return (float)((0.5 * (double)Math.Pow(2f, -10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) - 0.5) * ((double)end - (double)start)) + end;
        t -= 2f;
        return (float)((-0.5 * (double)Math.Pow(2f, 10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float InBounce(float start, float end, float t)
    {
        t = 1f - t;
        if ((double)t < 0.363636374473572)
            return (float)((-121.0 / 16.0 * (double)t * (double)t + 1.0) * ((double)end - (double)start)) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)((-121.0 / 16.0 * (double)t * (double)t - 0.75 + 1.0) * ((double)end - (double)start)) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)((-121.0 / 16.0 * (double)t * (double)t - 15.0 / 16.0 + 1.0) * ((double)end - (double)start)) + start;
        }
        t -= 0.9545454f;
        return (float)((-121.0 / 16.0 * (double)t * (double)t - 63.0 / 64.0 + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float OutBounce(float start, float end, float t)
    {
        if ((double)t < 0.363636374473572)
            return (float)(121.0 / 16.0 * (double)t * (double)t * ((double)end - (double)start)) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)((121.0 / 16.0 * (double)t * (double)t + 0.75) * ((double)end - (double)start)) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)((121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) * ((double)end - (double)start)) + start;
        }
        t -= 0.9545454f;
        return (float)((121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) * ((double)end - (double)start)) + start;
    }

    public static float InOutBounce(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            t = 1f - t;
            if ((double)t < 0.363636374473572)
                return (float)((-121.0 / 32.0 * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
            if ((double)t < 0.727272748947144)
            {
                t -= 0.5454546f;
                return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 0.5) * ((double)end - (double)start)) + start;
            }
            if ((double)t < 0.909090936183929)
            {
                t -= 0.8181818f;
                return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 0.5) * ((double)end - (double)start)) + start;
            }
            t -= 0.9545454f;
            return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 0.5) * ((double)end - (double)start)) + start;
        }
        --t;
        if ((double)t < 0.363636374473572)
            return (float)((121.0 / 32.0 * (double)t * (double)t + 0.5) * ((double)end - (double)start)) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)((0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 0.5) * ((double)end - (double)start)) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)((0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 0.5) * ((double)end - (double)start)) + start;
        }
        t -= 0.9545454f;
        return (float)((0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 0.5) * ((double)end - (double)start)) + start;
    }

    public static float OutInBounce(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            if ((double)t < 0.363636374473572)
                return (float)(121.0 / 32.0 * (double)t * (double)t * ((double)end - (double)start)) + start;
            if ((double)t < 0.727272748947144)
            {
                t -= 0.5454546f;
                return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) * ((double)end - (double)start)) + start;
            }
            if ((double)t < 0.909090936183929)
            {
                t -= 0.8181818f;
                return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) * ((double)end - (double)start)) + start;
            }
            t -= 0.9545454f;
            return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) * ((double)end - (double)start)) + start;
        }
        t = 2f - t;
        if ((double)t < 0.363636374473572)
            return (float)((-121.0 / 32.0 * (double)t * (double)t + 1.0) * ((double)end - (double)start)) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 1.0) * ((double)end - (double)start)) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 1.0) * ((double)end - (double)start)) + start;
        }
        t -= 0.9545454f;
        return (float)((-0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 1.0) * ((double)end - (double)start)) + start;
    }

    public static float InBack(float start, float end, float t)
    {
        return (float)((double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742) * ((double)end - (double)start)) + start;
    }

    public static float OutBack(float start, float end, float t)
    {
        --t;
        return (float)((1.0 - (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * ((double)end - (double)start)) + start;
    }

    public static float InOutBack(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742) * ((double)end - (double)start)) + start;
        t -= 2f;
        return (float)((1.0 - 0.5 * (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * ((double)end - (double)start)) + start;
    }

    public static float OutInBack(float start, float end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)((0.5 - 0.5 * (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * ((double)end - (double)start)) + start;
        }
        --t;
        return (float)((0.5 * (double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742) + 0.5) * ((double)end - (double)start)) + start;
    }

    public static Vector3 EaseByType(EasingUtility.Easing e, Vector3 start, Vector3 end, float t)
    {
        switch (e)
        {
            case EasingUtility.Easing.Linear:
                return EasingUtility.Linear(start, end, t);
            case EasingUtility.Easing.InQuad:
                return EasingUtility.InQuad(start, end, t);
            case EasingUtility.Easing.OutQuad:
                return EasingUtility.OutQuad(start, end, t);
            case EasingUtility.Easing.InOutQuad:
                return EasingUtility.InOutQuad(start, end, t);
            case EasingUtility.Easing.OutInQuad:
                return EasingUtility.OutInQuad(start, end, t);
            case EasingUtility.Easing.InCubic:
                return EasingUtility.InCubic(start, end, t);
            case EasingUtility.Easing.OutCubic:
                return EasingUtility.OutCubic(start, end, t);
            case EasingUtility.Easing.InOutCubic:
                return EasingUtility.InOutCubic(start, end, t);
            case EasingUtility.Easing.OutInCubic:
                return EasingUtility.OutInCubic(start, end, t);
            case EasingUtility.Easing.InQuart:
                return EasingUtility.InQuart(start, end, t);
            case EasingUtility.Easing.OutQuart:
                return EasingUtility.OutQuart(start, end, t);
            case EasingUtility.Easing.InOutQuart:
                return EasingUtility.InOutQuart(start, end, t);
            case EasingUtility.Easing.OutInQuart:
                return EasingUtility.OutInQuart(start, end, t);
            case EasingUtility.Easing.InQuint:
                return EasingUtility.InQuint(start, end, t);
            case EasingUtility.Easing.OutQuint:
                return EasingUtility.OutQuint(start, end, t);
            case EasingUtility.Easing.InOutQuint:
                return EasingUtility.InOutQuint(start, end, t);
            case EasingUtility.Easing.OutInQuint:
                return EasingUtility.OutInQuint(start, end, t);
            case EasingUtility.Easing.InSin:
                return EasingUtility.InSin(start, end, t);
            case EasingUtility.Easing.OutSin:
                return EasingUtility.OutSin(start, end, t);
            case EasingUtility.Easing.InOutSin:
                return EasingUtility.InOutSin(start, end, t);
            case EasingUtility.Easing.OutInSin:
                return EasingUtility.OutInSin(start, end, t);
            case EasingUtility.Easing.InExp:
                return EasingUtility.InExp(start, end, t);
            case EasingUtility.Easing.OutExp:
                return EasingUtility.OutExp(start, end, t);
            case EasingUtility.Easing.InOutExp:
                return EasingUtility.InOutExp(start, end, t);
            case EasingUtility.Easing.OutInExp:
                return EasingUtility.OutInExp(start, end, t);
            case EasingUtility.Easing.InCirc:
                return EasingUtility.InCirc(start, end, t);
            case EasingUtility.Easing.OutCirc:
                return EasingUtility.OutCirc(start, end, t);
            case EasingUtility.Easing.InOutCirc:
                return EasingUtility.InOutCirc(start, end, t);
            case EasingUtility.Easing.OutInCirc:
                return EasingUtility.OutInCirc(start, end, t);
            case EasingUtility.Easing.InElastic:
                return EasingUtility.InElastic(start, end, t);
            case EasingUtility.Easing.OutElastic:
                return EasingUtility.OutElastic(start, end, t);
            case EasingUtility.Easing.InOutElastic:
                return EasingUtility.InOutElastic(start, end, t);
            case EasingUtility.Easing.OutInElastic:
                return EasingUtility.OutInElastic(start, end, t);
            case EasingUtility.Easing.InBounce:
                return EasingUtility.InBounce(start, end, t);
            case EasingUtility.Easing.OutBounce:
                return EasingUtility.OutBounce(start, end, t);
            case EasingUtility.Easing.InOutBounce:
                return EasingUtility.InOutBounce(start, end, t);
            case EasingUtility.Easing.OutInBounce:
                return EasingUtility.OutInBounce(start, end, t);
            case EasingUtility.Easing.InBack:
                return EasingUtility.InBack(start, end, t);
            case EasingUtility.Easing.OutBack:
                return EasingUtility.OutBack(start, end, t);
            case EasingUtility.Easing.InOutBack:
                return EasingUtility.InOutBack(start, end, t);
            case EasingUtility.Easing.OutInBack:
                return EasingUtility.OutInBack(start, end, t);
            default:
                return Vector3.zero;
        }
    }

    public static Vector3 Linear(Vector3 start, Vector3 end, float t)
    {
        return t * (end - start) + start;
    }

    public static Vector3 InQuad(Vector3 start, Vector3 end, float t)
    {
        return t * t * (end - start) + start;
    }

    public static Vector3 OutQuad(Vector3 start, Vector3 end, float t)
    {
        return (float)-((double)t * ((double)t - 2.0)) * (end - start) + start;
    }

    public static Vector3 InOutQuad(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return 0.5f * t * t * (end - start) + start;
        --t;
        return (float)(-0.5 * ((double)t * ((double)t - 2.0) - 1.0)) * (end - start) + start;
    }

    public static Vector3 OutInQuad(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(-0.5 * ((double)t * ((double)t - 2.0) - 1.0) - 0.5) * (end - start) + start;
        --t;
        return (float)(0.5 * (double)t * (double)t + 0.5) * (end - start) + start;
    }

    public static Vector3 InCubic(Vector3 start, Vector3 end, float t)
    {
        return t * t * t * (end - start) + start;
    }

    public static Vector3 OutCubic(Vector3 start, Vector3 end, float t)
    {
        --t;
        return (float)((double)t * (double)t * (double)t + 1.0) * (end - start) + start;
    }

    public static Vector3 InOutCubic(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return 0.5f * t * t * t * (end - start) + start;
        t -= 2f;
        return (float)(0.5 * ((double)t * (double)t * (double)t + 2.0)) * (end - start) + start;
    }

    public static Vector3 OutInCubic(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(0.5 * ((double)t * (double)t * (double)t + 2.0) - 0.5) * (end - start) + start;
        }
        --t;
        return (float)(0.5 * (double)t * (double)t * (double)t + 0.5) * (end - start) + start;
    }

    public static Vector3 InQuart(Vector3 start, Vector3 end, float t)
    {
        return t * t * t * t * (end - start) + start;
    }

    public static Vector3 OutQuart(Vector3 start, Vector3 end, float t)
    {
        --t;
        return (float)-((double)t * (double)t * (double)t * (double)t - 1.0) * (end - start) + start;
    }

    public static Vector3 InOutQuart(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return 0.5f * t * t * t * t * (end - start) + start;
        t -= 2f;
        return (float)(-0.5 * ((double)t * (double)t * (double)t * (double)t - 2.0)) * (end - start) + start;
    }

    public static Vector3 OutInQuart(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(-0.5 * (double)t * (double)t * (double)t * (double)t + 0.5) * (end - start) + start;
        }
        --t;
        return (float)(0.5 * (double)t * (double)t * (double)t * (double)t + 0.5) * (end - start) + start;
    }

    public static Vector3 InQuint(Vector3 start, Vector3 end, float t)
    {
        return t * t * t * t * t * (end - start) + start;
    }

    public static Vector3 OutQuint(Vector3 start, Vector3 end, float t)
    {
        --t;
        return (float)((double)t * (double)t * (double)t * (double)t * (double)t + 1.0) * (end - start) + start;
    }

    public static Vector3 InOutQuint(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return 0.5f * t * t * t * t * t * (end - start) + start;
        t -= 2f;
        return (float)(0.5 * (double)t * (double)t * (double)t * (double)t * (double)t + 1.0) * (end - start) + start;
    }

    public static Vector3 OutInQuint(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(0.5 * ((double)t * (double)t * (double)t * (double)t * (double)t) + 0.5) * (end - start) + start;
        }
        --t;
        return (float)(0.5 * (double)t * (double)t * (double)t * (double)t * (double)t + 0.5) * (end - start) + start;
    }

    public static Vector3 InSin(Vector3 start, Vector3 end, float t)
    {
        return (float)-Math.Cos(((double)t * 3.14159274101257 * 0.5)) * (end - start) + end;
    }

    public static Vector3 OutSin(Vector3 start, Vector3 end, float t)
    {
        return (float)Math.Sin(((double)t * 3.14159274101257 * 0.5)) * (end - start) + start;
    }

    public static Vector3 InOutSin(Vector3 start, Vector3 end, float t)
    {
        return (float)(-0.5 * (double)Math.Cos(t * 3.141593f) + 0.5) * (end - start) + start;
    }

    public static Vector3 OutInSin(Vector3 start, Vector3 end, float t)
    {
        return (t - ((float)(-0.5 * (double)Math.Cos(t * 3.141593f) + 0.5) - t)) * (end - start) + start;
    }

    public static Vector3 InExp(Vector3 start, Vector3 end, float t)
    {
        return (float)Math.Pow(2f, (10.0 * ((double)t - 1.0))) * (end - start) + start;
    }

    public static Vector3 OutExp(Vector3 start, Vector3 end, float t)
    {
        return (float)(1f - Math.Pow(2f, -10f * t)) * (end - start) + start;
    }

    public static Vector3 InOutExp(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return 0.5f * (float)Math.Pow(2f, (10.0 * ((double)t - 1.0))) * (end - start) + start;
        --t;
        return (float)(0.5 * (2.0 - (double)Math.Pow(2f, -10f * t))) * (end - start) + start;
    }

    public static Vector3 OutInExp(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (2.0 - (double)Math.Pow(2f, -10f * t)) - 0.5) * (end - start) + start;
        --t;
        return (float)(0.5 * (double)Math.Pow(2f, (float)(10.0 * ((double)t - 1.0))) + 0.5) * (end - start) + start;
    }

    public static Vector3 InCirc(Vector3 start, Vector3 end, float t)
    {
        return (float)-((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) - 1.0) * (end - start) + start;
    }

    public static Vector3 OutCirc(Vector3 start, Vector3 end, float t)
    {
        --t;
        return (float)Math.Sqrt((1.0 - (double)t * (double)t)) * (end - start) + start;
    }

    public static Vector3 InOutCirc(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(-0.5 * ((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) - 1.0)) * (end - start) + start;
        t -= 2f;
        return (float)(0.5 * ((double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) + 1.0)) * (end - start) + start;
    }

    public static Vector3 OutInCirc(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return 0.5f * (float)Math.Sqrt((1.0 - (double)t * (double)t)) * (end - start) + start;
        }
        --t;
        return (float)(-0.5 * (double)Math.Sqrt((float)(1.0 - (double)t * (double)t)) + 1.0) * (end - start) + start;
    }

    public static Vector3 InElastic(Vector3 start, Vector3 end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        --t;
        return -(float)Math.Pow(2f, 10f * t) * (float)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * (end - start) + start;
    }

    public static Vector3 OutElastic(Vector3 start, Vector3 end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        return (float)Math.Pow(2f, -10f * t) * (float)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * (end - start) + end;
    }

    public static Vector3 InOutElastic(Vector3 start, Vector3 end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return -0.5f * (float)Math.Pow(2f, 10f * t) * (float)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * (end - start) + start;
        }
        --t;
        return 0.5f * (float)Math.Pow(2f, -10f * t) * (float)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) * (end - start) + end;
    }

    public static Vector3 OutInElastic(Vector3 start, Vector3 end, float t)
    {
        float num1 = 0.3f;
        float num2 = 0.075f;
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)Math.Pow(2f, -10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) - 0.5) * (end - start) + end;
        t -= 2f;
        return (float)(-0.5 * (double)Math.Pow(2f, 10f * t) * (double)Math.Sin((float)(((double)t - (double)num2) * 6.28318548202515) / num1) + 0.5) * (end - start) + start;
    }

    public static Vector3 InBounce(Vector3 start, Vector3 end, float t)
    {
        t = 1f - t;
        if ((double)t < 0.363636374473572)
            return (float)(-121.0 / 16.0 * (double)t * (double)t + 1.0) * (end - start) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)(-121.0 / 16.0 * (double)t * (double)t - 0.75 + 1.0) * (end - start) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)(-121.0 / 16.0 * (double)t * (double)t - 15.0 / 16.0 + 1.0) * (end - start) + start;
        }
        t -= 0.9545454f;
        return (float)(-121.0 / 16.0 * (double)t * (double)t - 63.0 / 64.0 + 1.0) * (end - start) + start;
    }

    public static Vector3 OutBounce(Vector3 start, Vector3 end, float t)
    {
        if ((double)t < 0.363636374473572)
            return 121f / 16f * t * t * (end - start) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)(121.0 / 16.0 * (double)t * (double)t + 0.75) * (end - start) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)(121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) * (end - start) + start;
        }
        t -= 0.9545454f;
        return (float)(121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) * (end - start) + start;
    }

    public static Vector3 InOutBounce(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            t = 1f - t;
            if ((double)t < 0.363636374473572)
                return (float)(-121.0 / 32.0 * (double)t * (double)t + 0.5) * (end - start) + start;
            if ((double)t < 0.727272748947144)
            {
                t -= 0.5454546f;
                return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 0.5) * (end - start) + start;
            }
            if ((double)t < 0.909090936183929)
            {
                t -= 0.8181818f;
                return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 0.5) * (end - start) + start;
            }
            t -= 0.9545454f;
            return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 0.5) * (end - start) + start;
        }
        --t;
        if ((double)t < 0.363636374473572)
            return (float)(121.0 / 32.0 * (double)t * (double)t + 0.5) * (end - start) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 0.5) * (end - start) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 0.5) * (end - start) + start;
        }
        t -= 0.9545454f;
        return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 0.5) * (end - start) + start;
    }

    public static Vector3 OutInBounce(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            if ((double)t < 0.363636374473572)
                return 121f / 32f * t * t * (end - start) + start;
            if ((double)t < 0.727272748947144)
            {
                t -= 0.5454546f;
                return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75)) * (end - start) + start;
            }
            if ((double)t < 0.909090936183929)
            {
                t -= 0.8181818f;
                return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0)) * (end - start) + start;
            }
            t -= 0.9545454f;
            return (float)(0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0)) * (end - start) + start;
        }
        t = 2f - t;
        if ((double)t < 0.363636374473572)
            return (float)(-121.0 / 32.0 * (double)t * (double)t + 1.0) * (end - start) + start;
        if ((double)t < 0.727272748947144)
        {
            t -= 0.5454546f;
            return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 0.75) + 1.0) * (end - start) + start;
        }
        if ((double)t < 0.909090936183929)
        {
            t -= 0.8181818f;
            return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 15.0 / 16.0) + 1.0) * (end - start) + start;
        }
        t -= 0.9545454f;
        return (float)(-0.5 * (121.0 / 16.0 * (double)t * (double)t + 63.0 / 64.0) + 1.0) * (end - start) + start;
    }

    public static Vector3 InBack(Vector3 start, Vector3 end, float t)
    {
        return (float)((double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742)) * (end - start) + start;
    }

    public static Vector3 OutBack(Vector3 start, Vector3 end, float t)
    {
        --t;
        return (float)(1.0 - (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * (end - start) + start;
    }

    public static Vector3 InOutBack(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
            return (float)(0.5 * (double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742)) * (end - start) + start;
        t -= 2f;
        return (float)(1.0 - 0.5 * (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * (end - start) + start;
    }

    public static Vector3 OutInBack(Vector3 start, Vector3 end, float t)
    {
        t *= 2f;
        if ((double)t < 1.0)
        {
            --t;
            return (float)(0.5 - 0.5 * (double)t * (double)t * (-2.70158004760742 * (double)t - 1.70158004760742)) * (end - start) + start;
        }
        --t;
        return (float)(0.5 * (double)t * (double)t * (2.70158004760742 * (double)t - 1.70158004760742) + 0.5) * (end - start) + start;
    }

    public enum Easing
    {
        Linear,
        InQuad,
        OutQuad,
        InOutQuad,
        OutInQuad,
        InCubic,
        OutCubic,
        InOutCubic,
        OutInCubic,
        InQuart,
        OutQuart,
        InOutQuart,
        OutInQuart,
        InQuint,
        OutQuint,
        InOutQuint,
        OutInQuint,
        InSin,
        OutSin,
        InOutSin,
        OutInSin,
        InExp,
        OutExp,
        InOutExp,
        OutInExp,
        InCirc,
        OutCirc,
        InOutCirc,
        OutInCirc,
        InElastic,
        OutElastic,
        InOutElastic,
        OutInElastic,
        InBounce,
        OutBounce,
        InOutBounce,
        OutInBounce,
        InBack,
        OutBack,
        InOutBack,
        OutInBack,
    }
}
