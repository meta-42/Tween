using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum EasingType
{
    Linear,
    Spring,
    EaseInQuad,
    EaseOutQuad,
    EaseInOutQuad,
    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,
    EaseInQuart,
    EaseOutQuart,
    EaseInOutQuart,
    EaseInQuint,
    EaseOutQuint,
    EaseInOutQuint,
    EaseInSin,
    EaseOutSin,
    EaseInOutSin,
    EaseInExp,
    EaseOutExp,
    EaseInOutExp,
    EaseInCirc,
    EaseOutCirc,
    EaseInOutCirc,
    EaseInBack,
    EaseOutBack,
    EaseInOutBack,
    EaseInElastic,
    EaseOutElastic,
    EaseInOutElastic,
    EaseInBounce,
    EaseOutBounce,
    EaseInOutBounce,
}

public enum TweenState
{
    Playing,
    Paused,
    Stopped,
}

public interface ITween
{
    void Play();
    void Pause();
    void Stop();
    bool OnTick(float elapsedTime);
}

public class Tween<T> : ITween where T : struct
{
    public T start;
    public T end;
    public T value;
    public EasingType type;
    public float duration;
    public float currentTime;
    public GameObject go;
    public Action<Tween<T>> onComplete;
    public Action<Tween<T>> onPlaying;

    TweenState state = TweenState.Stopped;

    public void Play()
    {
        if (state != TweenState.Playing)
        {
            state = TweenState.Playing;
        }
    }

    public void Pause()
    {
        if (state == TweenState.Playing)
        {
            state = TweenState.Paused;
        }
    }

    public void Stop()
    {
        if (state == TweenState.Stopped)
        {
            return;
        }

        state = TweenState.Stopped;
        currentTime = duration;
        if (onComplete != null)
        {
            onComplete.Invoke(this);
            onComplete = null;
        }
    }

    public virtual bool OnTick(float elapsedTime)
    {
        return state == TweenState.Playing;
    }

    public float Evaluate(float start, float end, float value)
    {
        switch (type)
        {
            case EasingType.Linear:
                return Easing.Linear(start, end, value);
            case EasingType.Spring:
                return Easing.Spring(start, end, value);
            case EasingType.EaseInQuad:
                return Easing.EaseInQuad(start, end, value);
            case EasingType.EaseOutQuad:
                return Easing.EaseOutQuad(start, end, value);
            case EasingType.EaseInOutQuad:
                return Easing.EaseInOutQuad(start, end, value);
            case EasingType.EaseInCubic:
                return Easing.EaseInCubic(start, end, value);
            case EasingType.EaseOutCubic:
                return Easing.EaseOutCubic(start, end, value);
            case EasingType.EaseInOutCubic:
                return Easing.EaseInOutCubic(start, end, value);
            case EasingType.EaseInQuart:
                return Easing.EaseInQuart(start, end, value);
            case EasingType.EaseOutQuart:
                return Easing.EaseOutQuart(start, end, value);
            case EasingType.EaseInOutQuart:
                return Easing.EaseInOutQuart(start, end, value);
            case EasingType.EaseInQuint:
                return Easing.EaseInQuint(start, end, value);
            case EasingType.EaseOutQuint:
                return Easing.EaseOutQuint(start, end, value);
            case EasingType.EaseInOutQuint:
                return Easing.EaseInOutQuint(start, end, value);
            case EasingType.EaseInSin:
                return Easing.EaseInSin(start, end, value);
            case EasingType.EaseOutSin:
                return Easing.EaseOutSin(start, end, value);
            case EasingType.EaseInOutSin:
                return Easing.EaseInOutSin(start, end, value);
            case EasingType.EaseInExp:
                return Easing.EaseInExp(start, end, value);
            case EasingType.EaseOutExp:
                return Easing.EaseOutExp(start, end, value);
            case EasingType.EaseInOutExp:
                return Easing.EaseInOutExp(start, end, value);
            case EasingType.EaseInCirc:
                return Easing.EaseInCirc(start, end, value);
            case EasingType.EaseOutCirc:
                return Easing.EaseOutCirc(start, end, value);
            case EasingType.EaseInOutCirc:
                return Easing.EaseInOutCirc(start, end, value);
            case EasingType.EaseInBack:
                return Easing.EaseInBack(start, end, value);
            case EasingType.EaseOutBack:
                return Easing.EaseOutBack(start, end, value);
            case EasingType.EaseInOutBack:
                return Easing.EaseInOutBack(start, end, value);
            case EasingType.EaseInElastic:
                return Easing.EaseInElastic(start, end, value);
            case EasingType.EaseOutElastic:
                return Easing.EaseOutElastic(start, end, value);
            case EasingType.EaseInOutElastic:
                return Easing.EaseInOutElastic(start, end, value);
            case EasingType.EaseInBounce:
                return Easing.EaseInBounce(start, end, value);
            case EasingType.EaseOutBounce:
                return Easing.EaseOutBounce(start, end, value);
            case EasingType.EaseInOutBounce:
                return Easing.EaseInOutBounce(start, end, value);
            default:
                throw new NotSupportedException("Not supported EasingType " + type);
        }
    }
}

public class FloatTween : Tween<float>, ITween
{
    private float GetValue(float from, float to, float value)
    {
        return Evaluate(from, to, value);
    }

    public override bool OnTick(float elapsedTime)
    {
        currentTime += elapsedTime;
        if (currentTime >= duration)
        {
            Stop();
        }
        else
        {
            value = GetValue(start, end, currentTime / duration);
            onPlaying(this);
        }

        return base.OnTick(elapsedTime);
    }
}

public class Vector2Tween : Tween<Vector2>
{
    private Vector2 GetValue(Vector2 from, Vector2 to, float value)
    {
        Vector2 v = new Vector2();
        v.x = Evaluate(from.x, to.x, value);
        v.y = Evaluate(from.y, to.y, value);
        return v;
    }

    public override bool OnTick(float elapsedTime)
    {
        currentTime += elapsedTime;
        if (currentTime >= duration)
        {
            Stop();
        }
        else
        {
            value = GetValue(start, end, currentTime / duration);
            onPlaying(this);
        }
        return base.OnTick(elapsedTime);
    }
}

public class Vector3Tween : Tween<Vector3>
{
    private Vector3 GetValue(Vector3 from, Vector3 to, float value)
    {
        Vector3 v = new Vector3();
        v.x = Evaluate(from.x, to.x, value);
        v.y = Evaluate(from.y, to.y, value);
        v.z = Evaluate(from.z, to.z, value);
        return v;
    }

    public override bool OnTick(float elapsedTime)
    {
        currentTime += elapsedTime;
        if (currentTime >= duration)
        {
            Stop();
        }
        else
        {
            value = GetValue(start, end, currentTime / duration);
            onPlaying(this);
        }
        return base.OnTick(elapsedTime);
    }
}



public class ETween : MonoBehaviour
{
    static List<ITween> tweens = new List<ITween>();

    public static void Move(GameObject go, Vector3 from, Vector3 to, float duration, EasingType type, Action<Tween<Vector3>> onComplete)
    {
        Vector3Tween t = new Vector3Tween();
        t.start = from;
        t.end = to;
        t.type = type;
        t.duration = duration;
        t.go = go;
        t.onPlaying = (tween) => { tween.go.transform.position = tween.value; };
        t.onComplete = onComplete;
        ETween.AddTween(t);
        t.Play();
    }

    public static void AddTween(ITween tween)
    {
        tweens.Add(tween);
    }

    public static void RemoveTween(ITween tween)
    {
        tween.Stop();
        tweens.Remove(tween);
    }

    void Update()
    {
        ITween t;
        for (int i = tweens.Count - 1; i >= 0; i--)
        {
            t = tweens[i];
            if (!t.OnTick(Time.deltaTime) && i < tweens.Count && tweens[i] == t)
            {
                tweens.RemoveAt(i);
            }
        }
    }
}
// ETween.Move(gameObject, Vector3.zero, new Vector3(2, 0, 0), 1, EasingType.EaseOutBounce, (t) => { Debug.Log("Complete"); });