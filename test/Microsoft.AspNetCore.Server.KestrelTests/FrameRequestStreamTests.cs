﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Http;
using Xunit;

namespace Microsoft.AspNetCore.Server.KestrelTests
{
    public class FrameRequestStreamTests
    {
        [Fact]
        public void CanReadReturnsTrue()
        {
            var stream = new FrameRequestStream();
            Assert.True(stream.CanRead);
        }

        [Fact]
        public void CanSeekReturnsFalse()
        {
            var stream = new FrameRequestStream();
            Assert.False(stream.CanSeek);
        }

        [Fact]
        public void CanWriteReturnsFalse()
        {
            var stream = new FrameRequestStream();
            Assert.False(stream.CanWrite);
        }

        [Fact]
        public void SeekThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.Seek(0, SeekOrigin.Begin));
        }

        [Fact]
        public void LengthThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.Length);
        }

        [Fact]
        public void SetLengthThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.SetLength(0));
        }

        [Fact]
        public void PositionThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.Position);
            Assert.Throws<NotSupportedException>(() => stream.Position = 0);
        }

        [Fact]
        public void WriteThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.Write(new byte[1], 0, 1));
        }

        [Fact]
        public void WriteByteThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.WriteByte(0));
        }

        [Fact]
        public async Task WriteAsyncThrows()
        {
            var stream = new FrameRequestStream();
            await Assert.ThrowsAsync<NotSupportedException>(() => stream.WriteAsync(new byte[1], 0, 1));
        }

#if NET451
        [Fact]
        public void BeginWriteThrows()
        {
            var stream = new FrameRequestStream();
            Assert.Throws<NotSupportedException>(() => stream.BeginWrite(new byte[1], 0, 1, null, null));
        }
#endif

        [Fact]
        public void FlushDoesNotThrow()
        {
            var stream = new FrameRequestStream();
            stream.Flush();
        }

        [Fact]
        public async Task FlushAsyncDoesNotThrow()
        {
            var stream = new FrameRequestStream();
            await stream.FlushAsync();
        }

        [Fact]
        public void AbortCausesReadToCancel()
        {
            var stream = new FrameRequestStream();
            stream.StartAcceptingReads(null);
            stream.Abort();
            var task = stream.ReadAsync(new byte[1], 0, 1);
            Assert.True(task.IsCanceled);
        }
    }
}
