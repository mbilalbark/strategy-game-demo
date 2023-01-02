using STGD.Core.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonModel : MonoBehaviour
{
    [SerializeField]
    private ProductionType type;
    [SerializeField]
    private Building building;

    public ProductionType Type { get => type; set => type = value; }
    public Building Building { get => building; set => building = value; }

    public enum ProductionType
    {
        Barrack,
        PowerPlant
    }
}
