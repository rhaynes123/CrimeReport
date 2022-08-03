using System;
namespace CrimeReport.Models.Interfaces
{
    public interface ICosmosEntity
    {
        /// <summary>
        /// Unique Id For Every Cosmos Entity
        /// </summary>
         string Id { get; set; }
        /// <summary>
        /// Partion Key to be set per entity logic
        /// </summary>
         string PartionKey { get; set; }
    }
}

