using System;
using System.Text.Json.Serialization;

namespace RaspberryPresenceStatus.Models.Enuns
{
    /// <summary>
    /// Presence status
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PresenceStatusEnum
    {
        /// <summary>
        /// Avaliable
        /// </summary>
        Avaliable,
        /// <summary>
        /// Away
        /// </summary>
        Away,
        /// <summary>
        /// Busy
        /// </summary>
        Busy,
        /// <summary>
        /// Do not disturb
        /// </summary>
        DoNotDisturb,
        /// <summary>
        /// Offline
        /// </summary>
        Offline
    }
}