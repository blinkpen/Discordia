using System;
using System.Collections.Generic;
using System.Text;

namespace Discordia
{
    class warrior : @base
    {
        ///<summary>
        ///multiplies strength of player
        ///</summary> 
        public int strengthMultiplier = 5;
        
        ///<summary>
        ///Attack with melee
        ///</summary> 
        public void AttackMelee()
        {
            strength *= strengthMultiplier;           
        }
    }
}
