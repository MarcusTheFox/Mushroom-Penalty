using UnityEngine;

namespace Core.Interfaces
{
    public interface IConfigurable<in TConfig> where TConfig : ScriptableObject
    {
        void Configure(TConfig config);
    }
}