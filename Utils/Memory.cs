using System.Runtime.InteropServices;

namespace FFXIVConnector.Utils
{
    public static class Memory
    {
        public static T ToStruct<T>(byte[] bytes) where T : struct
        {
            if (bytes == null || bytes.Length < Marshal.SizeOf<T>())
            {
                throw new ArgumentException("The byte array is not large enough for the target structure.");
            }

            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return Marshal.PtrToStructure<T>(handle.AddrOfPinnedObject());
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert byte array to structure.", ex);
            }
            finally
            {
                handle.Free();
            }
        }
    }
}