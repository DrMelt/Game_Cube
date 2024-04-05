using Godot;
using System;


namespace GameKernel
{
    public partial class EndPoint : CubeBase
    {
        public delegate void EnteredEndPoint();
        public static event EnteredEndPoint EnteredEndPointEvent;

        public override void EnteredCube(Player player)
        {
            EnteredEndPointEvent.Invoke();
        }

        public override bool CanEntry(Player player)
        {
            return true;
        }
    }
}
