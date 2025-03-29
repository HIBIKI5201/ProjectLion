using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FadeSystem : MonoBehaviour
{
    public static FadeSystem Instance;
    private static readonly Dictionary<FadeMode, string> _fadeDict = new Dictionary<FadeMode, string>();
    
    [SerializeField] string _fadeInAnimation = "FadeIn";
    [SerializeField] string _fadeOutAnimation = "FadeOut";
    
    Animator _animator;
    AnimatorStateInfo _stateInfo;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        _fadeDict.Add(FadeMode.FadeIn, _fadeInAnimation);
        _fadeDict.Add(FadeMode.FadeOut, _fadeOutAnimation);
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    }

    public async Task Fade(FadeMode mode, Action callback = null, CancellationToken ct = default)
    {
        _animator.Play(_fadeDict[mode], -1, 0);
        await Awaitable.WaitForSecondsAsync(_stateInfo.length, ct);
        callback?.Invoke();
    }

    public enum FadeMode
    {
        FadeIn,
        FadeOut
    }
}
