using System;
using WB.Tweener;

public interface ITweener
{
    bool IsRunning { get; }
    void Play();
    void Sleep();
    ITweener SetEase(Ease ease);
}

internal interface ITweenerUpdater : ITweener
{
    void Update(float deltaTime);
}

internal interface ITweenerForSequence : ITweener
{
    void AddOnCompleted(Action onCompleted);
    void SetNextNode(ITweener node);
    void AddJoinNode(ITweener node);
}