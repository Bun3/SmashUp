#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Q8KqHqj2wH6aQX3vLJeBDZWsl07YdLp0Bf/z8/f38sKQw/nC+/Txp3nreywLuZ4H9VnQwvAa6swKovshhpqdgJuGi8Pkwub08af28eH/s4JMBoFpHCCW/TmLvcYqUMwLig2ZOpbH0ee556vvQWYFBG5sPaJIM6qiZ2yI/la1eakm5MXBOTb9vzzmmyNF6U9hsNbg2DX970S/bqyROrly5YvSk4GBh5+XgdKTkZGXgoaTnJGXm5SbkZOGm52c0rOHhpqdgJuGi8PSsbPCcPPQwv/0+9h0unQF//Pz856X0ruckdzD1MLW9PGn9vnh77OC5MLm9PGn9vHh/7OCgp6X0qCdnYaCnpfSoJ2dhtKxs8Ls5f/CxMLGwHLm2SKbtWaE+wwGmX/cslQFtb+NiMJw84TC/PTxp+/98/MN9vbx8PP/9PvYdLp0Bf/z8/f38vFw8/PyrnDz8vT72HS6dAWRlvfzwnMAwtj07Xdxd+lrz7XFAFtpsnzeJkNi4CrP1JXSeMGYBf9wPSwZUd0LoZiplkfIXwb9/PJg+UPT5NyGJ87/KZDk3LJUBbW/jfqswu308afv0fbqwuTHwMPGwsHEqOX/wcfCwMLLwMPGwvb04fCnocPhwuP08af2+OH4s4KC0p2U0oaal9KGmpec0pOCgp6bkZPUwtb08af2+eHvs4KCnpfSsZeAhn2Bc5I06an73WBACra6ApLKbOcH1hAZI0WCLf23E9U4A5+KHxVH5eX0wv308afv4fPzDfb3wvHz8w3C75yW0pGdnJabhpudnIHSnZTSh4GX3cJzMfT62fTz9/f18PDCc0Toc0HCcPZJwnDxUVLx8PPw8PPwwv/0+/Uej8txeaHSIco2Q01ovfiZDdkO+tn08/f39fDz5OyahoaCgcjd3YW7KoRtweaXU4VmO9/w8fPy81Fw84ablJuRk4aX0pCL0pOci9KCk4CGWVGDYLWhpzNd3bNBCgkRgj8UUb4rxI0zdacrVWtLwLAJKieDbIxToI2zWmoLIziUbtaZ4yJRSRbp2DHtWi6M0Mc41ycr/SSZJlDW0eMFU14ykcGFBcj13qQZKP3T/ChIgeu9R97SkZeAhpuUm5GThpfSgp2em5GL+qzCcPPj9PGn79L2cPP6wnDz9sKFhdyTgoKel9yRnZ/dk4KCnpeRkzvrgAev/CeNrWkA1/FIp32/r/8DkJ6X0oGGk5yWk4CW0oaXgJ+B0pOgl56bk5yRl9KdnNKGmpuB0pGXgMHEqMKQw/nC+/Txp/b04fCnocPht4ztvpmiZLN7NoaQ+eJxs3XBeHOrVff7juWypOPshiFFedHJtVEnncLj9PGn9vjh+LOCgp6X0ruckdzDlX36RtIFOV7e0p2CRM3zwn5FsT308afv/Pbk9ubZIpu1ZoT7DAaZf4CTkYabkZfSgYaThpefl5yGgdzC/W/PAdm72ug6DDxHS/wrrO4kOc/38vFw8/3ywnDz+PBw8/PyFmNb++1jKey1ohn3H6yLdt8ZxFClvqcexGu+34pFH35pLgGFaQCEIIXCvTPSk5yW0pGXgIablJuRk4abnZzSgoKel9Kxl4CGm5SbkZOGm52c0rOHolh4JygWDiL79cVCh4fT");
        private static int[] order = new int[] { 26,1,16,5,50,20,18,59,40,11,13,20,12,18,32,28,47,34,49,56,23,31,59,47,41,49,55,53,29,43,37,55,51,35,53,39,41,58,53,54,57,44,54,44,56,54,53,57,49,52,57,55,58,53,59,55,56,58,59,59,60 };
        private static int key = 242;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
