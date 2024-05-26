using Godot;

namespace Nordwind {
    public class Util {

        public static Color getColor(int type) {
            switch (type) {
                case 0:
                    return Colors.Red;
                case 1:
                    return Colors.Blue;
                case 2:
                    return Colors.Green;
                case -1:
                    return Colors.Black;
                default:
                    return Colors.Red;
            }
        }

    }
}