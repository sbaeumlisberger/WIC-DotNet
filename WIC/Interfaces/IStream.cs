using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IStream)]
    public partial interface IStream : ISequentialStream
    {
        void Seek(
            long dlibMove,
            STREAM_SEEK dwOrigin,
            IntPtr plibNewPosition);

        void SetSize(
            long libNewSize);

        void CopyTo(
            IStream pstm,
            long cb,
            out long pcbRead,
            out long pcbWritten);

        void Commit(
            STGC grfCommitFlags);

        void Revert();

        void LockRegion(
            long libOffset,
            long cb,
            LOCKTYPE dwLockType);

        void UnlockRegion(
            long libOffset,
            long cb,
            LOCKTYPE dwLockType);

        void Stat(
            ref STATSTG pstatstg,
            STATFLAG grfStatFlag);

        IStream Clone();
    }
}
