#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("caxQ51g3mE3cOoU17caXWmly29kYcCBIRn78dYrx1hgO8I7hz8iEEeN2NUNL+wil5LTO0FJ88NCVL+kp0sWfXlbZf85vywWwYgt+pVtnh75QQzuqn7zHYfn5QZLSV4ZycWjOQkPgHGfKfbZjvy8d1a5EVnS92jGy6RTJMKu5YlIzJgxwYu29PaFrYX2OMucX79/tH70ySboJNAUGe3knzRgPZsoJOA3atUJIdJXRIxbIZ+cpK/2PopyMUkeNH8Bjyd0aPylX/XU/BRUH+gGhWx7czO8H61QT7MvG6RakJwQWKyAvDKBuoNErJycnIyYlj7vtILkrLiT+zbTfpPkonercuK+kJykmFqQnLCSkJycm6ZoeZ9Apu07rq89kG38F5yQlJyYn");
        private static int[] order = new int[] { 7,9,12,3,13,6,12,12,8,13,11,12,12,13,14 };
        private static int key = 38;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
