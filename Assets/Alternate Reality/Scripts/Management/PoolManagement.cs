using System.Collections.Generic;
using AlternateReality.Blocks;
using AlternateReality.Interfaces;
using UnityEngine;
using AlternateReality.FX;

namespace AlternateReality.Management
{
    public class PoolManagement : MonoBehaviour
    {
        public static PoolManagement Instance;
        
        private List<PoolObject> _objects;
        private Dictionary<string, Queue<GameObject>> _pool;

        
        private void Awake()
        {
            Instance = this;
            
            _objects = new List<PoolObject>
            {
                new PoolObject(Block.REGULAR_BLOCK, Resources.Load<GameObject>(Block.REGULAR_BLOCK), 100),
                new PoolObject(Block.EXPLOSIVE_BLOCK, Resources.Load<GameObject>(Block.EXPLOSIVE_BLOCK), 100),
                new PoolObject(Fx.EXPLOSION_FX, Resources.Load<GameObject>(Fx.EXPLOSION_FX), 100)
            };
            
            _pool = new Dictionary<string, Queue<GameObject>>();

            foreach (PoolObject po in _objects)
            {
                Queue<GameObject> queue = new Queue<GameObject>();

                for (int i = 0; i < po.amount; i++)
                {
                    GameObject go = Instantiate(po.gameObject, transform);
                    go.name = po.name;
                    go.SetActive(false);
                    queue.Enqueue(go);
                }
                
                _pool.Add(po.name, queue);
            }
        }


        public GameObject Spawn(string name, Vector3 position)
        {
            if (!_pool.ContainsKey(name))
            {
                Debug.LogWarning(this + $": Spawn(string, Vector3) -> Unable to find key: '{ name }'.");
                return null;
            }

            if (_pool[name].Count <= 0)
            {
                Debug.LogWarning(this + $": Spawn(string, Vector3) -> Queue '{ name }' is empty. Unable to spawn from pool.");
                return null;
            }
            
            GameObject go = _pool[name].Dequeue(); 
            go.transform.SetParent(null);
            go.transform.position = position;
            go.SetActive(true);
            
            IPoolable i = go.GetComponent<IPoolable>();
            i?.Spawn();

            return go;
        }


        public GameObject Remove(GameObject go)
        {
            if (!_pool.ContainsKey(go.name))
            {
                Debug.LogWarning(this + $": Remove(GameObject) -> Unable to find key: '{ go.name }'.");
                return null;
            }
            
            _pool[go.name].Enqueue(go);

            IPoolable i = go.GetComponent<IPoolable>();
            i?.Remove();
            
            go.transform.SetParent(transform);
            go.SetActive(false);
            go.transform.position = transform.position;

            return go;
        }


        public void Dispose()
        {
            
        }
    }
}