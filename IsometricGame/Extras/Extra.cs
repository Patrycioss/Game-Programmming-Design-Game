﻿namespace GXPEngine.Extras
{
    /// <summary>
    /// Static class to add useful functions
    /// </summary>
    public static class Extra
    {
        public static float Remap (this float value, float from1, float to1, float from2, float to2) 
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}