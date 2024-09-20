using System.Runtime.InteropServices;

namespace MemoryEditingSoftware.Core
{
    /// <summary>
    /// Exports memory access functionalities from the C++ library.
    /// For reading and writing data.
    /// </summary>
    public static class MemoryAccess
    {
        #region Initialization

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int InitLink(string TargetProcessName);

        #endregion

        #region Read

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern double ReadDoubleFromMemory(string address);

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReadIntFromMemory(string address);

        #endregion

        #region Write

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteDoubleInMemory(string address, double value);

        [DllImport(@"MemoryManipulation.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int WriteIntInMemory(string address, int value);

        #endregion
    }
}
