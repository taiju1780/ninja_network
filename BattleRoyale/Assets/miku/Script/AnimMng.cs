using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimMng : SingletonMonoBehaviour<AnimMng>
{
    private Animator playerAnim;
    private string animNameWalk = "isWalk";
    private string animNameRun = "isRun";
    private string animNameJump = "isJump";
    private string animNamePunch = "isPunch";

    private void Start()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    //再生メソッド
    public void WalkPlay()
    {
        playerAnim.SetBool(animNameWalk, true);
    }
    public void RunPlay()
    {
        playerAnim.SetBool(animNameRun, true);
    }
    public void JumpPlay()
    {
        playerAnim.SetBool(animNameJump, true);
    }
    public void PunchPlay()
    {
        playerAnim.SetBool(animNamePunch, true);
    }

    //停止メソッド
    public void WalkStop()
    {
        playerAnim.SetBool(animNameWalk, false);
    }
    public void RunStop()
    {
        playerAnim.SetBool(animNameRun, false);
    }
    public void JumpStop()
    {
        playerAnim.SetBool(animNameJump, false);
    }
    public void PunchStop()
    {
        playerAnim.SetBool(animNamePunch, false);
    }
    public void AllStop()
    {
        WalkStop();
        RunStop();
        JumpStop();
        PunchStop();
    }
}
