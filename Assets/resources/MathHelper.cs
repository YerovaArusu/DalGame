using System.Collections;
using System.Collections.Generic;
using System;

public class MathHelper {
    
    
    public static double getRandomDouble(double minimum, double maximum)
    { 
        Random random = new Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }
    
    public static float getRandomFloat(float minimum, float maximum)
    {
        return (float) getRandomDouble(minimum, maximum);
    }
}
