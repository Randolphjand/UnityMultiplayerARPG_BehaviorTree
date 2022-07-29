using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Cysharp.Threading.Tasks;  //possibly for async

namespace KitBehaviorTree
{
    public static class TaskExtensions //for async
    {
        public static async void Await(this Task task, Action<Exception> errorCallback)
        {
            try
            {
                await task;
            }

            catch (Exception e)
            {
                errorCallback?.Invoke(e);
            }
        }
    }

    public class BehaviourTreeRunner : MonoBehaviour
    {
        // The main behaviour tree scriptable object asset
        [Tooltip("This is the SO Behavior Tree you want to use to control the behavior of this entity.")]
        public BehaviourTree tree;
        // Storage container object to hold game object subsystems
        Context context;
        //[Tooltip("If you use Ticks:  This tree will not update every frame for performance and variety in visual AI behavior." +
        //    " Each tree will have it's own update rate between the two settings which will be randomized at start.")]
        //[SerializeField] private bool useTicks = true;
        //private float tickTimer;
        [SerializeField]
        [Tooltip("This tree will not update every frame for performance and variety in visual AI behavior." +
            " Each tree will have it's own update rate between the two settings which will be randomized at start.")]
        [Min(0.01f)]
        private float minTickTime = 0.2f;
        [SerializeField]
        private float maxTickTime = 1f;
        private float waitForSeconds;
        private CancellationTokenSource tokenSource = null;  //for async
        //private CancellationTokenSource _cancellationTokenSource;

        void Start()
        {
            tokenSource = new CancellationTokenSource();  //for async
            var token = tokenSource.Token;                //for async
            if (tree == null)
            {
                Debug.LogError("  Behaviour tree scriptable object is required on  " + gameObject.name + " ");
            }
            context = CreateBehaviourTreeContext();
            tree = tree.Clone();
            tree.Bind(context);
            //waitForSeconds = UnityEngine.Random.Range(minTickTime, maxTickTime);
            //StartCoroutine(RunTreeCoRout());
            waitForSeconds = UnityEngine.Random.Range(minTickTime * 1000, maxTickTime * 1000);  //async time is in millisecs
            //RunTreeAsync((int)waitForSeconds, token).Await(HandleError);
            RunTreeUnitaskAsync(this.tokenSource.Token).Forget();
        }
        private async UniTask RunTreeUnitaskAsync(CancellationToken cancellationToken)
        {
            Debug.Log(" Start UniTask DoSomethingAsync");
            while (true)
            {
                //tree.Update();
                await UniTask.Delay((int)waitForSeconds, cancellationToken: cancellationToken);
                //await UniTask.Delay((int)waitForSeconds);
                //await UniTask.Delay((int)waitForSeconds, cancellationToken);
                tree.Update();
                // ---
                //if (cancellationToken.IsCancellationRequested)
                //{
                //    Debug.Log(" ThrowIfCancellationRequested");
                //}

                // OR

                cancellationToken.ThrowIfCancellationRequested(); // breaks further execution of this method
                // ---

                //Debug.Log(" ThrowIfCancellationRequested");
            }
            
        }
        //void Update()
        //{
        //    if (!useTicks)
        //    {
        //        tree.Update();
        //    }
        //    else
        //    {
        //        tickTimer += Time.time;
        //        if (tickTimer >= waitForSeconds)
        //        {
        //            tickTimer = 0;
        //            tree.Update();
        //        }
        //    }
        //}
        IEnumerator RunTreeCoRout()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(waitForSeconds);
                tree.Update();
            }
        }
        Context CreateBehaviourTreeContext()
        {
            return Context.CreateFromGameObject(gameObject);
        }
        private void OnDrawGizmosSelected()
        {
            if (!tree)
            {
                return;
            }

            BehaviourTree.Traverse(tree.rootNode, (n) =>
            {
                if (n.drawGizmos)
                {
                    n.OnDrawGizmos();
                }
            });
        }
        private void HandleError(Exception ex)
        {
            //This can be commented out.
            Debug.Log("From BehaviorTreeRunner  (This message can be commented out!  " + ex.Message);
        }
        private async Task RunTreeAsync(int waitSeconds, CancellationToken ctoken)  //for c# async
        {
            while (true)
            {
                await Task.Delay(waitSeconds);
                if (ctoken.IsCancellationRequested)
                {
                    ctoken.ThrowIfCancellationRequested();
                }
                tree.Update();
            }
        }

        //void OnApplicationQuit()
        //{
        //    Debug.Log("  From onapplicationquit");
        //    CancellationTokenSource ctoken = new CancellationTokenSource();
        //    tokenSource.Cancel();
        //    return;
        //}
        private void OnDestroy()
        {
            Debug.Log("  From BT OnDestroy...");

            CancellationTokenSource ctoken = new CancellationTokenSource();
            tokenSource.Cancel();
            return;
        }
    }
}