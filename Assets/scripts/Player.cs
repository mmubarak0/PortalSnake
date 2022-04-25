using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Player 
{
    public static int score;
    int speed;
    string name;
    int Level;
    int InitSize = 3;
    Vector2 direction = Vector2.right;
    
   public Player(string name="guest", int speed = 3)
   {
       this.name = name;
       this.speed = speed;
   }

   public int getScore(bool sc=true)
   {
       // return The score if the bool sc is true
       Debug.Log("The Player name is " + name);
       Debug.Log("The Score is : " + score);
       if (sc)
       { return score; }
       return 0;
   }

   public void setScore(int sco)
   {
       score = sco;
   }

   public void setSpeed(int sp)
   {
       // if you need to modify the speed
       speed = sp;
   }

   public int getSpeed()
   {
       // return the player speed 
       return speed;
   }

   public bool isDead()
   {
       // this function should test if the player is dead or not
       if (score == (-1))
       {
           Debug.Log(name + " is Dead");
           return true;
       }
       else 
       {
           Debug.Log(name + " is alive");
           return false;
       }
   }

   public bool deserveBonus()
   {
       // this function check if the player deserve a bonus or not 
       if (score%5 == 0)
       {
           Debug.Log(name + " deserve a bonus");
           return true;
       }
       else
       {
           Debug.Log(name + "  dosen't deserve a bonus");
           return false;
       }
   }

    public Vector2 Direction(string di) {
        if (di == "up"  && direction != Vector2.right)
        {
            direction = Vector2.up;
        }
        else if (di == "down" && direction != Vector2.right)
        {
            direction = Vector2.down;
        }
        else if (di == "right" && direction != Vector2.right)
        {
            direction = Vector2.right;
        }
        else if (di == "left" && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        return direction;
    }

    public Vector2 getDirection() {
        return direction;
    }

    public void setLevel(int level)
    {
        Level = level;
    }
    public void setInitSize(int initsize)
    {
        InitSize = initsize;
    }

    public int getLevel()
    {
        return Level;
    }
    public int getInitSize()
    {
        return InitSize;
    }

}
