using System;
using QRCode;
using UnityEngine;

public class StateTest : State
{
    public override void OnStateEnter(EventArgs arg)
    {
        if (arg is WrapperArgs<MyArgDeTest> args)
        {
            Debug.Log("ENTER " + StateName + " " + + args.Arg.a);
        }
    }

    public override void OnStateExecute()
    {
        Debug.Log("EXECUTE " + StateName);
    }

    public override void OnStateExit(EventArgs arg)
    {
        if (arg is WrapperArgs<MyArgDeTest> args)
        {
            Debug.Log("FINISH " + StateName + " " +  args.Arg.a);
        }
    }
}

public class MyArgDeTest
{
    public float a;
}
