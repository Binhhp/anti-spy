﻿using Newtonsoft.Json.Converters;
using System;
using Newtonsoft.Json;
using WixSharp.Enums;
using System.Runtime.Serialization;
using System.Reflection;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WixSharp.Converters
{
    /// <summary>
    /// A custom converter that returns null if an error occurs while deserializing
    /// </summary>
    public class NullOnErrorConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return serializer.Deserialize(reader, objectType);
            }
            catch
            {
                return null;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException($"Unnecessary because {nameof(CanWrite)} is false.");
        }
    }
}
