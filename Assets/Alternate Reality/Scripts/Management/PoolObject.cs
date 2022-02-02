using UnityEngine;

namespace AlternateReality.Management
{
    public class PoolObject
    {
        public string name;
        public GameObject gameObject;
        public int amount;

        public PoolObject(string name, GameObject go, int amount)
        {
            this.name = name;
            this.gameObject = go;
            this.amount = amount;
        }
    }
}