using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.PubSub;

namespace STGD.Core.Settings
{
    [CreateAssetMenu(fileName = "Map", menuName = "STGD/MapSettings")]
    public class MapSettings : ScriptableObject
    {
        [SerializeField] private int gridSizeX;
        [SerializeField] private int gridSizeY;
        [SerializeField] private Transform mapTile;
        public int GridSizeX { get => gridSizeX; }
        public int GridSizeY { get => gridSizeY;}
        public Transform MapTile { get => mapTile; }
    }
}
