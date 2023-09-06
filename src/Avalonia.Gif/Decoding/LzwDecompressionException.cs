// Licensed under the MIT License.
// Copyright (C) 2018 Jumar A. Macato, All Rights Reserved.

using System;
using System.Runtime.Serialization;

namespace Avalonia.Gif.Decoding
{
    [Serializable]
    public class LzwDecompressionException : Exception
    {
        public LzwDecompressionException()
        {
        }

        public LzwDecompressionException(string message) : base(message)
        {
        }

        public LzwDecompressionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LzwDecompressionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}