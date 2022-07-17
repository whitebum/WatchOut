using UnityEngine;

[CreateAssetMenu(menuName = "Audio/BGM", fileName = "New BGM", order = int.MaxValue)]
public sealed class BGM : Audio
{
    public bool isLoop;
    public float loopStart;
    public float loopEnd;
}