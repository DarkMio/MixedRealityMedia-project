
namespace Utilities {
    public class IntVector2 {
        public int x;
        public int y;

        public IntVector2(int x, int y) {
            this.x = x;
            this.y = y;
        }

        int sqrMagnitude {
            get { return x * x + y * y; }
        }
    }
}