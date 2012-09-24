using System;
using System.Linq;

namespace SirenOfShame
{
    public class HeightAnimator
    {
        public const int HALF_SECOND_ANIMATION_SPEED = 16;

        private static readonly int[] _easingIncrease = new[] { 1, 2, 3, 4, 5, 7, 9, 11, 13, 15, 17, 20, 23, 26, 29, 32, 35, 38, 41, 44, 47, 50, 53, 56, 59, 62, 65, 69, 73, 77, 82, 87, 93, 99, int.MaxValue };

        public static int IncreaseWithEase(int oldValue, int destination)
        {
            int nextValue = _easingIncrease.FirstOrDefault(i => i > oldValue);
            if (nextValue == default(int)) nextValue = destination;
            return Math.Min(nextValue, destination);
        }
    }
}
