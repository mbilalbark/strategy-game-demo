using STGD.Core.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonModel : MonoBehaviour
{
    [SerializeField]
    private Building building;
    public Building Building { get => building; set => building = value; }

}
