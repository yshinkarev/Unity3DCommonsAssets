using UnityEngine;

namespace Commons.Interfaces
{
    public interface IGetGameObjects
    {
        GameObject[] GetGameObjects(params Object[] args);
    }
}