using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STGD.Core.PubSub;

namespace STGD.Core.Base
{
    public class Soldier: Unit
    {
       // public Publisher<SetRoad> SetRoadEvent;
        public void Move()
        {

        }

        public void SetPath()
        {
            //SetRoadEvent.Publish();
        }
    }

    public class SetRoad
    {
        public Queue<Vector3> Road { get; set; }
    }
}
