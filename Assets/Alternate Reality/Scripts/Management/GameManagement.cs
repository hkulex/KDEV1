using System;
using System.Collections.Generic;
using System.Linq;
using AlternateReality.Blocks;
using AlternateReality.View;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AlternateReality.Management
{
    public class GameManagement : MonoBehaviour
    {
        public static GameManagement Instance;
        
        private const int FIELD_WIDTH = 11;
        private const int FIELD_HEIGHT = 8;
        private const int EXPLOSIVE_PER_ROW = 3;

        private List<BaseBlock> _blocks;
        private Vector3 _location;
        private int _score;
        
        private void Start()
        {
            Instance = this;
            
            EventManagement.OnHitBlockEvent += OnRemoveBlock;

            _blocks = new List<BaseBlock>();
            _location = new Vector3(0f, 0.5f, 5f);
            _score = 0;
            
            for (int row = 0; row < FIELD_HEIGHT; row++)
            {
                int index = Random.Range(0, FIELD_WIDTH - EXPLOSIVE_PER_ROW + 1);
                
                for (int column = 0; column < FIELD_WIDTH; column++)
                {
                    Vector3 position = new Vector3(-Mathf.Floor(FIELD_WIDTH / 2f) + _location.x + column, _location.y, _location.z + row);
                    bool explosive = column >= index && column < index + EXPLOSIVE_PER_ROW;
                    
                    GameObject go = PoolManagement.Instance.Spawn(explosive ? Block.EXPLOSIVE_BLOCK : Block.REGULAR_BLOCK, position);
                    BaseBlock bb = go.GetComponent<BaseBlock>();
                    
                    bb.SetPosition(row, column);
                    
                    _blocks.Add(bb);
                }
            }
        }

        private void OnRemoveBlock(BaseBlock block, int row, int column)
        {
            _blocks.Remove(block);
            _score += block.Score;
            
            block.Die();
            
            if (block.transform.name == Block.EXPLOSIVE_BLOCK)
            {
                List<BaseBlock> blocks = _blocks.Where(x => x.Row == row).Select(x => x).ToList();

                foreach (BaseBlock bb in blocks)
                {
                    _blocks.Remove(bb);
                    _score += bb.Score;
                    
                    bb.Die();
                }
            }

            if (_blocks.Count <= 0)
            {
                End();
            }
        }


        public void End()
        {
            EventManagement.OnHitBlockEvent -= OnRemoveBlock;
            
            ViewManagement.Instance.SetView(Views.END_VIEW);
            ViewManagement.Instance.ActiveView.Initialize(_score);
        }

        public int Score => _score;
    }
}