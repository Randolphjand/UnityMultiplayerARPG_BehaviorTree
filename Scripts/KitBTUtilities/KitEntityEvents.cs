using MultiplayerARPG;
using UnityEngine;

namespace KitBehaviorTree.Utilities
{
    public class KitEntityEvents : Singleton<KitEntityEvents>
    {
        protected KitEntityEvents() { }

        public string TestString = "Kit BT Messages";

        /// <summary>
        /// Delegagte used to send message from game object to game object for eventual use as a Behaviour Designer Event
        /// </summary>
        /// <param name="param1">[REQUIRED] Always a ulong ObjectID of the monster sending the message</param>
        /// <param name="param2">Optional, dependant on the message type</param>
        /// <param name="param3">Optional, dependant on the message type</param>
        public delegate void BehaviourEvent(object param1, object param2, object param3);

        public event BehaviourEvent OnAttacking;
        public event BehaviourEvent OnDied;
        public event BehaviourEvent OnFleeing;
        public event BehaviourEvent OnWaiting;
        public event BehaviourEvent OnGoingHome;
        public event BehaviourEvent OnNeedHealing;
        public event BehaviourEvent OnNeedBuffing;
        public event BehaviourEvent OnNeedHelp;

        #region Radio Messages
        public void SendRadioMessage(RadioMessageType messageType, object param1, object param2 = null, object param3 = null)
        { 
            switch (messageType)
            {
                case RadioMessageType.Attacking:
                    {
                        if (OnAttacking != null) OnAttacking(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.Died:
                    {
                        if (OnDied != null) OnDied(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.Fleeing:
                    {
                        if (OnFleeing != null) OnFleeing(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.Waiting:
                    {
                        if (OnWaiting != null) OnWaiting(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.GoingHome:
                    {
                        if (OnGoingHome != null) OnGoingHome(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.NeedHealing:
                    {
                        if (OnNeedHealing != null) OnNeedHealing(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.NeedBuffing:
                    {
                        if (OnNeedBuffing != null) OnNeedBuffing(param1, param2, param3);
                        break;
                    }
                case RadioMessageType.NeedHelp:
                    {
                        if (OnNeedHelp != null) OnNeedHelp(param1, param2, param3);
                        break;
                    }
            }
        }
        #endregion
    }
}