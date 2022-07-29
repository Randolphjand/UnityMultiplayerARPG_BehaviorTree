using UnityEngine;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public class ExtendedNpcEntity : NpcEntity
    {
		[Header("Movement Tools")]
        public float walkSpeed = 3.5f;
        public float runSpeed = 6.5f;

        public override sealed bool CanMove()
        {
            return true;
        }
        public override sealed float GetMoveSpeed()
        {
            return walkSpeed;
        }

        public override sealed bool CanSprint()
        {
            return true;
        }

        public override sealed bool CanCrouch()
        {
            return true;
        }

        public override sealed bool CanCrawl()
        {
            return true;
        }
	}
}