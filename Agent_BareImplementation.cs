using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent_BareImplementation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var model = ModelLoader.LoadFromStreamingAssets(RollerBallBrain + ".nn");
        var worker = BarracudaWorkerFactory.CreateWorker(BarracudaWorkerFactory.Type.ComputePrecompiled, model);
    }

    // Update is called once per frame
    void Update()
    {
        var inputs = new Dictionary<string, Tensor>();
        // Target and Agent positions
        inputs[Target_position] = new Tensor(Target.position);
        inputs[Agent_position] = new Tensor(this.transform.position);
        // Agent velocity
        inputs[Agent_velocity_x] = new Tensor(rBody.velocity.x);
        inputs[Agent_velocity_y] = new Tensor(rBody.velocity.z);

        worker.Execute(inputs);
        var O = worker.Peek(outputName);
        Debug.log(O);
        O.Dispose();
        worker.Dispose();

    }
}
